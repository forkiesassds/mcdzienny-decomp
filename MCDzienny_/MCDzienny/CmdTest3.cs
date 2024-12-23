using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MCDzienny
{
    public class CmdTest3 : Command
    {
        const int Port = 25568;

        static UdpClient receivingClient;

        static UdpClient sendingClient;

        static Thread receiver;

        readonly IPAddress MulticastAddress = IPAddress.Parse("239.0.0.222");

        IPEndPoint localEndpoint = new IPEndPoint(IPAddress.Any, 25568);

        public override string name { get { return "servermessage"; } }

        public override string shortcut { get { return "sm"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            if ((p.level.mapType == MapType.Zombie || p.level.mapType == MapType.Lava) && Server.voteMode)
            {
                Player.SendMessage(p, "You can't use this command during the voting. Wait a moment.");
                return;
            }
            if (p.muted)
            {
                Player.SendMessage(p, "You are currently muted and cannot use this command.");
                return;
            }
            if (Server.chatmod && !p.voice)
            {
                Player.SendMessage(p, "Chat moderation is on, you cannot talk.");
                return;
            }
            string s = "<SM>" + p.PublicName + ": " + message;
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            sendingClient.Send(bytes, bytes.Length);
        }

        public override void Init()
        {
            try
            {
                if (receivingClient != null)
                {
                    receivingClient.Close();
                }
                if (sendingClient != null)
                {
                    sendingClient.Close();
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
            InitializeReceiver();
            InitializeSender();
        }

        void InitializeSender()
        {
            sendingClient = CreateMulticastClient();
            IPEndPoint endPoint = new IPEndPoint(MulticastAddress, 25568);
            sendingClient.Connect(endPoint);
        }

        void InitializeReceiver()
        {
            receivingClient = CreateMulticastClient();
            receiver = new Thread(Receiver);
            receiver.IsBackground = true;
            receiver.Start();
        }

        UdpClient CreateMulticastClient()
        {
            UdpClient udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, optionValue: true);
            udpClient.Client.Bind(localEndpoint);
            udpClient.JoinMulticastGroup(MulticastAddress);
            return udpClient;
        }

        void Receiver()
        {
            try
            {
                while (true)
                {
                    byte[] bytes = receivingClient.Receive(ref localEndpoint);
                    string @string = Encoding.ASCII.GetString(bytes);
                    Player.GlobalMessage(@string);
                }
            }
            catch (ObjectDisposedException) {}
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/sm [message] - sends servers wide message.");
        }
    }
}