using System;
using System.Collections.Generic;
using SuperSocket.SocketBase;
using SuperWebSocket;
using SuperWebSocket.Protocol;

namespace MCDzienny.RemoteAccess
{
    public static class Listener
    {
        static int port;

        static int errorCount = 0;

        static volatile bool closing;

        static WebSocketServer ws;

        static readonly Dictionary<WebSocketSession, RemoteClient> sessions = new Dictionary<WebSocketSession, RemoteClient>();

        static void ws_NewSessionConnected(WebSocketSession session)
        {
            sessions.Add(session, new RemoteClient(session));
        }

        static void ws_NewMessageReceived(WebSocketSession session, string value)
        {
            Console.WriteLine(value);
        }

        static void ws_NewDataReceived(WebSocketSession session, byte[] value)
        {
            sessions[session].HandleIncomingData(value);
        }

        public static void Start(int port)
        {
            //IL_0000: Unknown result type (might be due to invalid IL or missing references)
            //IL_000a: Expected O, but got Unknown
            try
            {
                ws = new WebSocketServer();
                ((AppServerBase<WebSocketSession, IWebSocketFragment>)(object)ws).Setup(33434);
                ((WebSocketServer<WebSocketSession>)(object)ws).NewDataReceived += ws_NewDataReceived;
                ((AppServerBase<WebSocketSession, IWebSocketFragment>)(object)ws).NewSessionConnected += ws_NewSessionConnected;
                ((WebSocketServer<WebSocketSession>)(object)ws).NewMessageReceived += ws_NewMessageReceived;
                ((AppServerBase<WebSocketSession, IWebSocketFragment>)(object)ws).Start();
                closing = false;
                Server.s.Log("Remote console listener started.");
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public static void Stop()
        {
            try
            {
                closing = true;
                if (ws != null)
                {
                    ((AppServerBase<WebSocketSession, IWebSocketFragment>)(object)ws).Stop();
                    ((AppServerBase<WebSocketSession, IWebSocketFragment>)(object)ws).Dispose();
                    Server.s.Log("Remote console listener stopped.");
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }
    }
}