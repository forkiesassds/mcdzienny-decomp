using System;

namespace MCDzienny.Notification
{
    public class Item
    {

        readonly string author;

        readonly string content;

        readonly DateTime expiration;
        readonly string id;

        readonly Priority priority;

        readonly DateTime pubDate;

        public Item(string id, string content, Priority priority)
            : this(id, content, priority, default(DateTime)) {}

        public Item(string id, string content, Priority priority, DateTime expiration)
        {
            this.id = id;
            this.content = content;
            this.priority = priority;
            this.expiration = expiration;
        }

        public Item(string id, string content, Priority priority, string author, DateTime pubDate)
        {
            this.id = id;
            this.content = content;
            this.priority = priority;
            this.pubDate = pubDate;
            this.author = author;
        }

        public string ID { get { return id; } }

        public string Content { get { return content; } }

        public DateTime Expiration { get { return expiration; } }

        public Priority Priority { get { return priority; } }

        public DateTime PubDate { get { return pubDate; } }

        public string Author { get { return author; } }
    }
}