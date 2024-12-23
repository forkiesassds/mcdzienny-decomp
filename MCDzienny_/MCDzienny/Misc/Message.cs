using System;

namespace MCDzienny.Misc
{
    public class Message
    {
        readonly string[] message;

        public Message(string message)
        {
            if (message == null)
            {
                throw new ArgumentException("Value cannot be null.", "message");
            }
            this.message = message.Split(new char[1]
            {
                ' '
            }, StringSplitOptions.RemoveEmptyEntries);
        }

        public int Pointer { get; set; }

        public int Count { get { return message.Length; } }

        public string LastRead { get; private set; }

        public bool HasNext { get { return Pointer < Count; } }

        public string ReadString()
        {
            if (Pointer >= message.Length)
            {
                return null;
            }
            Pointer++;
            LastRead = message[Pointer - 1];
            return LastRead;
        }

        public string ReadStringLower()
        {
            if (Pointer >= message.Length)
            {
                return null;
            }
            Pointer++;
            LastRead = message[Pointer - 1].ToLower();
            return LastRead;
        }

        public string ReadToEnd()
        {
            string text = ReadString();
            if (text == null)
            {
                return null;
            }
            while (true)
            {
                string text2 = ReadString();
                if (text2 == null)
                {
                    break;
                }
                text = text + " " + text2;
            }
            LastRead = text;
            return text;
        }

        public bool IsNextInt()
        {
            if (Pointer >= message.Length)
            {
                return false;
            }
            int result;
            return int.TryParse(message[Pointer], out result);
        }

        public int ReadInt()
        {
            Pointer++;
            LastRead = message[Pointer - 1];
            return int.Parse(LastRead);
        }
    }
}