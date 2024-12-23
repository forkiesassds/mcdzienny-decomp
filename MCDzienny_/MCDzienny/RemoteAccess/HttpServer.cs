using System;
using System.IO;
using System.Net;
using System.Threading;

namespace MCDzienny.RemoteAccess
{
    public class HttpServer
    {

        static HttpListener listener;

        static readonly string securityFile = "<?xml version='1.0'?>\r\n<cross-domain-policy>\r\n        <allow-access-from domain=\"*\"/>\r\n</cross-domain-policy>";

        public static bool IsListening { get; private set; }

        public static void Start()
        {
            try
            {
                listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:80/");
                listener.Prefixes.Add("http://127.0.0.1:80/");
                listener.Start();
                IsListening = true;
                Server.s.Log("Http listening has started.");
                new Thread(HtmlListenerThread).Start();
            }
            catch (Exception ex)
            {
                IsListening = false;
                Server.s.Log("Http listening has failed.");
                Server.ErrorLog(ex);
            }
        }

        static void HtmlListenerThread()
        {
            try
            {
                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    string text = context.Request.Url.LocalPath.Replace("/", "");
                    string arg = context.Request.Url.Query.Replace("?", "");
                    Server.s.Log(string.Format("Received request for {0}?{1}", text, arg), systemMsg: true);
                    if (text == "crossdomain.xml")
                    {
                        StreamWriter streamWriter = new StreamWriter(context.Response.OutputStream);
                        streamWriter.Write(securityFile);
                        streamWriter.Flush();
                    }
                    context.Response.Close();
                }
            }
            catch (Exception ex)
            {
                IsListening = false;
                if (!(ex is HttpListenerException) || ((HttpListenerException)ex).ErrorCode != 995)
                {
                    Server.ErrorLog(ex);
                }
            }
        }

        public static void Stop()
        {
            if (listener != null)
            {
                try
                {
                    listener.Stop();
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
                IsListening = false;
            }
        }
    }
}