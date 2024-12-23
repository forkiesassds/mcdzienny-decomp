using System;
using System.Net.Sockets;
using System.Text;
using SuperSocket.SocketBase;
using SuperWebSocket;
using SuperWebSocket.Protocol;

namespace MCDzienny.RemoteAccess
{
    public class SendManager
    {
        static readonly ASCIIEncoding asci = new ASCIIEncoding();

        readonly RemoteClient rc;

        readonly WebSocketSession ws;

        public SendManager(RemoteClient sender, WebSocketSession ws)
        {
            rc = sender;
            this.ws = ws;
        }

        public void SendSalt(byte[] generatedSalt)
        {
            byte[] array = new byte[17]
            {
                100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };
            Array.Copy(generatedSalt, 0, array, 1, 16);
            Send(array);
        }

        public void SendSuccess()
        {
            Send(new byte[1]
            {
                44
            });
        }

        public void SendDisconnect(string reason)
        {
            byte[] array = new byte[65];
            array[0] = 1;
            byte[] bytes = asci.GetBytes(reason);
            bytes.CopyTo(array, 1);
            SendDisconnect(array);
        }

        public void SendPlayers(string playersString)
        {
            Send(FormMessage(SendCodes.Players, playersString));
        }

        public void SendMaps(string mapsString)
        {
            Send(FormMessage(SendCodes.Maps, mapsString));
        }

        public void AddPlayer(string playersName)
        {
            Send(FormMessage(SendCodes.AddPlayer, playersName));
        }

        public void RemovePlayer(string playersName)
        {
            Send(FormMessage(SendCodes.RemovePlayer, playersName));
        }

        public void AddMap(string mapsName)
        {
            Send(FormMessage(SendCodes.AddMap, mapsName));
        }

        public void RemoveMap(string mapsName)
        {
            Send(FormMessage(SendCodes.RemoveMap, mapsName));
        }

        public void SendUptime(TimeSpan uptime)
        {
            byte[] array = new byte[5]
            {
                4, 0, 0, 0, 0
            };
            BitConverter.GetBytes((int)uptime.TotalMinutes).CopyTo(array, 1);
            Send(array);
        }

        public void SendTimes(int time1, int time2)
        {
            byte[] array = new byte[9]
            {
                5, 0, 0, 0, 0, 0, 0, 0, 0
            };
            BitConverter.GetBytes(time1).CopyTo(array, 1);
            BitConverter.GetBytes(time2).CopyTo(array, 5);
            Send(array);
        }

        public void SendLag(int lag)
        {
            byte[] array = new byte[5]
            {
                6, 0, 0, 0, 0
            };
            BitConverter.GetBytes(lag).CopyTo(array, 1);
            Send(array);
        }

        public void SendGameType(Mode gameMode)
        {
            Send(new byte[2]
            {
                99, (byte)gameMode
            });
        }

        public void SendPing()
        {
            Send(new byte[1]
            {
                10
            });
        }

        public void SendInfoBundle(string infoBundle)
        {
            Send(FormMessage(SendCodes.InfoBundle, infoBundle));
        }

        public void SendLog(string log)
        {
            Send(FormMessage(SendCodes.Log, log));
        }

        public void SendCommandLog(string log)
        {
            Send(FormMessage(SendCodes.CommandLog, log));
        }

        void Send(byte[] message)
        {
            try
            {
                if (rc.IsConnected)
                {
                    ((AppSession<WebSocketSession, IWebSocketFragment>)(object)ws).Send(message, 0, message.Length);
                }
            }
            catch (Exception ex)
            {
                if (rc != null)
                {
                    rc.Disconnect();
                }
                if (ex is SocketException)
                {
                    if (((SocketException)ex).ErrorCode != 10054)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    Console.WriteLine(ex);
                }
            }
        }

        void SendDisconnect(byte[] message)
        {
            try
            {
                if (rc.IsConnected)
                {
                    ((AppSession<WebSocketSession, IWebSocketFragment>)(object)ws).Send(message, 0, message.Length);
                }
            }
            catch (Exception ex)
            {
                if (rc != null)
                {
                    rc.Disconnect();
                }
                if (ex is SocketException)
                {
                    if (((SocketException)ex).ErrorCode != 10054)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    Console.WriteLine(ex);
                }
            }
        }

        void SendDisconnectCallback(IAsyncResult result)
        {
            try
            {
                ((AppSession<WebSocketSession, IWebSocketFragment>)(object)ws).Close();
            }
            catch (Exception)
            {
                rc.Disconnect();
            }
        }

        byte[] FormMessage(SendCodes code, string message)
        {
            byte[] array = new byte[message.Length + 3];
            array[0] = (byte)code;
            BitConverter.GetBytes((ushort)message.Length).CopyTo(array, 1);
            asci.GetBytes(message).CopyTo(array, 3);
            return array;
        }
    }
}