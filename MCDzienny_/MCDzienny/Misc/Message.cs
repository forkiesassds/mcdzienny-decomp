using System;

namespace MCDzienny.Misc
{
    // Token: 0x020001BB RID: 443
    public class Message
    {
        // Token: 0x0400067F RID: 1663
        private readonly string[] message;

        // Token: 0x04000680 RID: 1664

        // Token: 0x06000C93 RID: 3219 RVA: 0x00048CB8 File Offset: 0x00046EB8
        public Message(string message)
        {
            if (message == null) throw new ArgumentException("Value cannot be null.", "message");
            this.message = message.Split(new[]
            {
                ' '
            }, StringSplitOptions.RemoveEmptyEntries);
        }

        // Token: 0x170004E1 RID: 1249
        // (get) Token: 0x06000C8D RID: 3213 RVA: 0x00048C74 File Offset: 0x00046E74
        // (set) Token: 0x06000C8E RID: 3214 RVA: 0x00048C7C File Offset: 0x00046E7C
        public int Pointer { get; set; }

        // Token: 0x170004E2 RID: 1250
        // (get) Token: 0x06000C8F RID: 3215 RVA: 0x00048C88 File Offset: 0x00046E88
        public int Count
        {
            get { return message.Length; }
        }

        // Token: 0x170004E3 RID: 1251
        // (get) Token: 0x06000C90 RID: 3216 RVA: 0x00048C94 File Offset: 0x00046E94
        // (set) Token: 0x06000C91 RID: 3217 RVA: 0x00048C9C File Offset: 0x00046E9C
        public string LastRead { get; private set; }

        // Token: 0x170004E4 RID: 1252
        // (get) Token: 0x06000C92 RID: 3218 RVA: 0x00048CA8 File Offset: 0x00046EA8
        public bool HasNext
        {
            get { return Pointer < Count; }
        }

        // Token: 0x06000C94 RID: 3220 RVA: 0x00048CF8 File Offset: 0x00046EF8
        public string ReadString()
        {
            if (Pointer >= message.Length) return null;
            Pointer++;
            LastRead = message[Pointer - 1];
            return LastRead;
        }

        // Token: 0x06000C95 RID: 3221 RVA: 0x00048D38 File Offset: 0x00046F38
        public string ReadStringLower()
        {
            if (Pointer >= message.Length) return null;
            Pointer++;
            LastRead = message[Pointer - 1].ToLower();
            return LastRead;
        }

        // Token: 0x06000C96 RID: 3222 RVA: 0x00048D88 File Offset: 0x00046F88
        public string ReadToEnd()
        {
            var text = ReadString();
            if (text == null) return null;
            for (;;)
            {
                var text2 = ReadString();
                if (text2 == null) break;
                text = text + " " + text2;
            }

            LastRead = text;
            return text;
        }

        // Token: 0x06000C97 RID: 3223 RVA: 0x00048DC4 File Offset: 0x00046FC4
        public bool IsNextInt()
        {
            int num;
            return Pointer < message.Length && int.TryParse(message[Pointer], out num);
        }

        // Token: 0x06000C98 RID: 3224 RVA: 0x00048DF8 File Offset: 0x00046FF8
        public int ReadInt()
        {
            Pointer++;
            LastRead = message[Pointer - 1];
            return int.Parse(LastRead);
        }
    }
}