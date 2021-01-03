using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x0200005F RID: 95
    public class CmdTest3 : Command
    {
        // Token: 0x04000172 RID: 370
        private const int Port = 25568;

        // Token: 0x04000175 RID: 373
        private static UdpClient receivingClient;

        // Token: 0x04000176 RID: 374
        private static UdpClient sendingClient;

        // Token: 0x04000177 RID: 375
        private static Thread receiver;

        // Token: 0x04000173 RID: 371
        private readonly IPAddress MulticastAddress = IPAddress.Parse("239.0.0.222");

        // Token: 0x04000174 RID: 372
        private IPEndPoint localEndpoint = new IPEndPoint(IPAddress.Any, 25568);

        // Token: 0x17000095 RID: 149
        // (get) Token: 0x06000248 RID: 584 RVA: 0x0000CE24 File Offset: 0x0000B024
        public override string name
        {
            get { return "servermessage"; }
        }

        // Token: 0x17000096 RID: 150
        // (get) Token: 0x06000249 RID: 585 RVA: 0x0000CE2C File Offset: 0x0000B02C
        public override string shortcut
        {
            get { return "sm"; }
        }

        // Token: 0x17000097 RID: 151
        // (get) Token: 0x0600024A RID: 586 RVA: 0x0000CE34 File Offset: 0x0000B034
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000098 RID: 152
        // (get) Token: 0x0600024B RID: 587 RVA: 0x0000CE3C File Offset: 0x0000B03C
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000099 RID: 153
        // (get) Token: 0x0600024C RID: 588 RVA: 0x0000CE40 File Offset: 0x0000B040
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x1700009A RID: 154
        // (get) Token: 0x0600024D RID: 589 RVA: 0x0000CE44 File Offset: 0x0000B044
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600024E RID: 590 RVA: 0x0000CE48 File Offset: 0x0000B048
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

            var s = "<SM>" + p.PublicName + ": " + message;
            var bytes = Encoding.ASCII.GetBytes(s);
            sendingClient.Send(bytes, bytes.Length);
        }

        // Token: 0x0600024F RID: 591 RVA: 0x0000CEFC File Offset: 0x0000B0FC
        public override void Init()
        {
            try
            {
                if (receivingClient != null) receivingClient.Close();
                if (sendingClient != null) sendingClient.Close();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            InitializeReceiver();
            InitializeSender();
        }

        // Token: 0x06000250 RID: 592 RVA: 0x0000CF54 File Offset: 0x0000B154
        private void InitializeSender()
        {
            sendingClient = CreateMulticastClient();
            var endPoint = new IPEndPoint(MulticastAddress, 25568);
            sendingClient.Connect(endPoint);
        }

        // Token: 0x06000251 RID: 593 RVA: 0x0000CF88 File Offset: 0x0000B188
        private void InitializeReceiver()
        {
            receivingClient = CreateMulticastClient();
            receiver = new Thread(Receiver);
            receiver.IsBackground = true;
            receiver.Start();
        }

        // Token: 0x06000252 RID: 594 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
        private UdpClient CreateMulticastClient()
        {
            var udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.Client.Bind(localEndpoint);
            udpClient.JoinMulticastGroup(MulticastAddress);
            return udpClient;
        }

        // Token: 0x06000253 RID: 595 RVA: 0x0000D004 File Offset: 0x0000B204
        private void Receiver()
        {
            try
            {
                for (;;)
                {
                    var bytes = receivingClient.Receive(ref localEndpoint);
                    var @string = Encoding.ASCII.GetString(bytes);
                    Player.GlobalMessage(@string);
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000254 RID: 596 RVA: 0x0000D060 File Offset: 0x0000B260
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/sm [message] - sends servers wide message.");
        }
    }
}