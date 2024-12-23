using System;
using System.Text;

namespace MCDzienny.RemoteAccess
{
    public static class HandleReceive
    {
        static readonly ASCIIEncoding asci = new ASCIIEncoding();

        public static void NewMessage(RemoteClient rc, byte[] message)
        {
            byte[] array = new byte[rc.partialBuffer.Length + message.Length];
            Buffer.BlockCopy(rc.partialBuffer, 0, array, 0, rc.partialBuffer.Length);
            Buffer.BlockCopy(message, 0, array, rc.partialBuffer.Length, message.Length);
            HandleMessage(rc, array);
        }

        public static void HandleMessage(RemoteClient rc, byte[] b)
        {
            byte[] array = b;
            try
            {
                while (array.Length > 0)
                {
                    int num = 0;
                    ReceiveCodes receiveCodes = (ReceiveCodes)array[0];
                    ReceiveCodes receiveCodes2 = receiveCodes;
                    if (receiveCodes2 != ReceiveCodes.Login)
                    {
                        if (receiveCodes2 != ReceiveCodes.Chat)
                        {
                            if (receiveCodes2 != ReceiveCodes.Hello)
                            {
                                goto IL_0037;
                            }
                            num = 32;
                        }
                        else
                        {
                            if (!rc.verified)
                            {
                                goto IL_0037;
                            }
                            num = -1;
                        }
                    }
                    else
                    {
                        num = 48;
                    }
                    if (num == -1)
                    {
                        if (array.Length <= 1)
                        {
                            break;
                        }
                        num = array[1] + 1;
                        if (num > 220)
                        {
                            rc.Disconnect("Message was too long.");
                            return;
                        }
                    }
                    if (array.Length > num)
                    {
                        byte[] array2 = new byte[num];
                        Buffer.BlockCopy(array, 1, array2, 0, num);
                        byte[] array3 = new byte[array.Length - num - 1];
                        Buffer.BlockCopy(array, num + 1, array3, 0, array.Length - num - 1);
                        array = array3;
                        switch (receiveCodes)
                        {
                            case ReceiveCodes.Hello:
                                HandleHello(rc, array2);
                                break;
                            case ReceiveCodes.Login:
                                HandleLogin(rc, array2);
                                break;
                            case ReceiveCodes.Chat:
                                HandleChat(rc, array2);
                                break;
                        }
                        continue;
                    }
                    break;
                    IL_0037:
                    rc.Disconnect("Unahndled message id : " + (int)receiveCodes);
                    return;
                }
            }
            catch (Exception ex)
            {
                if (rc != null)
                {
                    rc.Disconnect();
                }
                Server.ErrorLog(ex);
            }
            rc.partialBuffer = array;
        }

        static void HandleHello(RemoteClient rc, byte[] message)
        {
            string @string = asci.GetString(message);
            char[] trimChars = new char[1];
            rc.clientType = @string.Trim(trimChars);
            Server.s.Log("Client type : " + rc.clientType);
            rc.SendSalt();
        }

        static void HandleLogin(RemoteClient rc, byte[] message)
        {
            byte[] array = new byte[16];
            byte[] array2 = new byte[32];
            Array.Copy(message, 0, array, 0, 16);
            Array.Copy(message, 16, array2, 0, 32);
            string @string = asci.GetString(array);
            char[] trimChars = new char[1];
            rc.Verify(@string.Trim(trimChars), array2);
        }

        static void HandleChat(RemoteClient rc, byte[] message)
        {
            byte[] array = new byte[message.Length - 1];
            Array.Copy(message, 1, array, 0, message.Length - 1);
            rc.HandleChat(asci.GetString(array));
        }
    }
}