using System;
using System.Collections.Generic;
using System.Threading;
using MCDzienny;
using MonoTorrent.Common;

namespace MonoTorrent.Client
{
    // Token: 0x0200038C RID: 908
    public class MainLoop
    {
        // Token: 0x04000E2A RID: 3626
        private readonly ICache<DelegateTask> cache = new Cache<DelegateTask>(true).Synchronize();

        // Token: 0x04000E29 RID: 3625
        private readonly AutoResetEvent handle = new AutoResetEvent(false);

        // Token: 0x04000E2B RID: 3627
        private readonly Queue<DelegateTask> tasks = new Queue<DelegateTask>();

        // Token: 0x04000E2C RID: 3628
        internal Thread thread;

        // Token: 0x060019E5 RID: 6629 RVA: 0x000B6208 File Offset: 0x000B4408
        public MainLoop(string name)
        {
            thread = new Thread(Loop);
            thread.IsBackground = true;
            thread.Start();
        }

        // Token: 0x060019E6 RID: 6630 RVA: 0x000B6274 File Offset: 0x000B4474
        private void Loop()
        {
            for (;;)
            {
                DelegateTask delegateTask = null;
                lock (tasks)
                {
                    if (tasks.Count > 0) delegateTask = tasks.Dequeue();
                }

                if (delegateTask == null)
                {
                    handle.WaitOne();
                }
                else
                {
                    var flag = !delegateTask.IsBlocking;
                    delegateTask.Execute();
                    if (flag) cache.Enqueue(delegateTask);
                }

                Thread.Sleep(10);
            }
        }

        // Token: 0x060019E7 RID: 6631 RVA: 0x000B62FC File Offset: 0x000B44FC
        private void Queue(DelegateTask task)
        {
            Queue(task, Priority.Normal);
        }

        // Token: 0x060019E8 RID: 6632 RVA: 0x000B6308 File Offset: 0x000B4508
        private void Queue(DelegateTask task, Priority priority)
        {
            lock (tasks)
            {
                tasks.Enqueue(task);
                handle.Set();
            }
        }

        // Token: 0x060019E9 RID: 6633 RVA: 0x000B6354 File Offset: 0x000B4554
        public void Queue(MainLoopTask task)
        {
            var delegateTask = cache.Dequeue();
            delegateTask.Task = task;
            Queue(delegateTask);
        }

        // Token: 0x060019EA RID: 6634 RVA: 0x000B637C File Offset: 0x000B457C
        public void QueueWait(MainLoopTask task)
        {
            var delegateTask = cache.Dequeue();
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

        // Token: 0x060019EB RID: 6635 RVA: 0x000B63C4 File Offset: 0x000B45C4
        public object QueueWait(MainLoopJob task)
        {
            var delegateTask = cache.Dequeue();
            delegateTask.Job = task;
            object jobResult;
            try
            {
                QueueWait(delegateTask);
                jobResult = delegateTask.JobResult;
            }
            finally
            {
                cache.Enqueue(delegateTask);
            }

            return jobResult;
        }

        // Token: 0x060019EC RID: 6636 RVA: 0x000B6414 File Offset: 0x000B4614
        private void QueueWait(DelegateTask t)
        {
            t.WaitHandle.Reset();
            t.IsBlocking = true;
            if (Thread.CurrentThread == thread)
                t.Execute();
            else
                Queue(t, Priority.Highest);
            t.WaitHandle.WaitOne();
            if (t.StoredException != null) throw new TorrentException("Exception in mainloop", t.StoredException);
        }

        // Token: 0x0200038D RID: 909
        private class DelegateTask : ICacheable
        {
            // Token: 0x04000E2D RID: 3629

            // Token: 0x04000E2E RID: 3630

            // Token: 0x04000E2F RID: 3631

            // Token: 0x04000E30 RID: 3632

            // Token: 0x04000E31 RID: 3633

            // Token: 0x04000E32 RID: 3634

            // Token: 0x04000E33 RID: 3635

            // Token: 0x04000E34 RID: 3636

            // Token: 0x060019FA RID: 6650 RVA: 0x000B64F4 File Offset: 0x000B46F4
            public DelegateTask()
            {
                WaitHandle = new ManualResetEvent(false);
            }

            // Token: 0x170008FF RID: 2303
            // (get) Token: 0x060019ED RID: 6637 RVA: 0x000B6478 File Offset: 0x000B4678
            // (set) Token: 0x060019EE RID: 6638 RVA: 0x000B6480 File Offset: 0x000B4680
            public bool IsBlocking { get; set; }

            // Token: 0x17000900 RID: 2304
            // (get) Token: 0x060019EF RID: 6639 RVA: 0x000B648C File Offset: 0x000B468C
            // (set) Token: 0x060019F0 RID: 6640 RVA: 0x000B6494 File Offset: 0x000B4694
            public MainLoopJob Job { get; set; }

            // Token: 0x17000901 RID: 2305
            // (get) Token: 0x060019F1 RID: 6641 RVA: 0x000B64A0 File Offset: 0x000B46A0
            // (set) Token: 0x060019F2 RID: 6642 RVA: 0x000B64A8 File Offset: 0x000B46A8
            public Exception StoredException { get; set; }

            // Token: 0x17000902 RID: 2306
            // (get) Token: 0x060019F3 RID: 6643 RVA: 0x000B64B4 File Offset: 0x000B46B4
            // (set) Token: 0x060019F4 RID: 6644 RVA: 0x000B64BC File Offset: 0x000B46BC
            public MainLoopTask Task { get; set; }

            // Token: 0x17000903 RID: 2307
            // (get) Token: 0x060019F5 RID: 6645 RVA: 0x000B64C8 File Offset: 0x000B46C8
            // (set) Token: 0x060019F6 RID: 6646 RVA: 0x000B64D0 File Offset: 0x000B46D0
            public TimeoutTask Timeout { get; set; }

            // Token: 0x17000904 RID: 2308
            // (get) Token: 0x060019F7 RID: 6647 RVA: 0x000B64DC File Offset: 0x000B46DC
            public object JobResult { get; private set; }

            // Token: 0x17000905 RID: 2309
            // (get) Token: 0x060019F8 RID: 6648 RVA: 0x000B64E4 File Offset: 0x000B46E4
            public bool TimeoutResult { get; private set; }

            // Token: 0x17000906 RID: 2310
            // (get) Token: 0x060019F9 RID: 6649 RVA: 0x000B64EC File Offset: 0x000B46EC
            public ManualResetEvent WaitHandle { get; private set; }

            // Token: 0x060019FC RID: 6652 RVA: 0x000B65A4 File Offset: 0x000B47A4
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

            // Token: 0x060019FB RID: 6651 RVA: 0x000B6508 File Offset: 0x000B4708
            public void Execute()
            {
                try
                {
                    if (Job != null)
                        JobResult = Job();
                    else if (Task != null)
                        Task();
                    else if (Timeout != null) TimeoutResult = Timeout();
                }
                catch (Exception ex)
                {
                    StoredException = ex;
                    if (!IsBlocking) Server.ErrorLog(ex);
                }
                finally
                {
                    WaitHandle.Set();
                }
            }
        }
    }
}