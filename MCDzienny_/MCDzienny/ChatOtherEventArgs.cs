using System;

namespace MCDzienny
{
    public class ChatOtherEventArgs : EventArgs
    {

        public ChatOtherEventArgs(string message, Player from, Player to, ChatType chatType)
        {
            Message = message;
            From = from;
            To = to;
            ChatType = chatType;
        }
        public Player From { get; set; }

        public Player To { get; set; }

        public bool Handled { get; set; }

        public ChatType ChatType { get; set; }

        public string Message { get; set; }
    }
}