using System;
using System.Collections.Generic;
using System.Threading;
using MCDzienny;
using MonoTorrent.Common;

namespace MonoTorrent.Client
{
    public class MainLoop
    {

        readonly ICache<DelegateTask> cache = new Cache<DelegateTask>(autoCreate: true).Synchronize();

        readonly AutoResetEvent handle = new AutoResetEvent(initialState: false);

        readonly Queue<DelegateTask> tasks = new Queue<DelegateTask>();

        internal Thread thread;

        public MainLoop(string name)
        {
            thread = new Thread(Loop);
            thread.IsBackground = true;
            thread.Start();
        }

        void Loop()
        {
            while (true)
            {
                DelegateTask delegateTask = null;
                lock (tasks)
                {
                    if (tasks.Count > 0)
                    {
                        delegateTask = tasks.Dequeue();
                    }
                }
                if (delegateTask == null)
                {
                    handle.WaitOne();
                }
                else
                {
                    bool flag = !delegateTask.IsBlocking;
                    delegateTask.Execute();
                    if (flag)
                    {
                        cache.Enqueue(delegateTask);
                    }
                }
                Thread.Sleep(10);
            }
        }

        void Queue(DelegateTask task)
        {
            Queue(task, Priority.Normal);
        }

        void Queue(DelegateTask task, Priority priority)
        {
            lock (tasks)
            {
                tasks.Enqueue(task);
                handle.Set();
            }
        }

        public void Queue(MainLoopTask task)
        {
            DelegateTask delegateTask = cache.Dequeue();
            delegateTask.Task = task;
            Queue(delegateTask);
        }

        public void QueueWait(MainLoopTask task)
        {
            DelegateTask delegateTask = cache.Dequeue();
            delegateTask.Task = task;
            try
            {
                QueueWait(delegateTask);
            }
            finally
            {
                cache.Enqueue(delegateTask);
            }
        }

        public object QueueWait(MainLoopJob task)
        {
            DelegateTask delegateTask = cache.Dequeue();
            delegateTask.Job = task;
            try
            {
                QueueWait(delegateTask);
                return delegateTask.JobResult;
            }
            finally
            {
                cache.Enqueue(delegateTask);
            }
        }

        void QueueWait(DelegateTask t)
        {
            t.WaitHandle.Reset();
            t.IsBlocking = true;
            if (Thread.CurrentThread == thread)
            {
                t.Execute();
            }
            else
            {
                Queue(t, Priority.Highest);
            }
            t.WaitHandle.WaitOne();
            if (t.StoredException != null)
            {
                throw new TorrentException("Exception in mainloop", t.StoredException);
            }
        }

        class DelegateTask : ICacheable
        {
            readonly ManualResetEvent handle;

            public DelegateTask()
            {
                handle = new ManualResetEvent(initialState: false);
            }

            public bool IsBlocking { get; set; }

            public MainLoopJob Job { get; set; }

            public Exception StoredException { get; set; }

            public MainLoopTask Task { get; set; }

            public TimeoutTask Timeout { get; set; }

            public object JobResult { get; private set; }

            public bool TimeoutResult { get; private set; }

            public ManualResetEvent WaitHandle { get { return handle; } }

            public void Initialise()
            {
                IsBlocking = false;
                Job = null;
                JobResult = null;
                StoredException = null;
                Task = null;
                Timeout = null;
                TimeoutResult = false;
            }

            public void Execute()
            {
                try
                {
                    if (Job != null)
                    {
                        JobResult = Job();
                    }
                    else if (Task != null)
                    {
                        Task();
                    }
                    else if (Timeout != null)
                    {
                        TimeoutResult = Timeout();
                    }
                }
                catch (Exception ex)
                {
                    Exception ex2 = StoredException = ex;
                    if (!IsBlocking)
                    {
                        Server.ErrorLog(ex2);
                    }
                }
                finally
                {
                    handle.Set();
                }
            }
        }
    }
}