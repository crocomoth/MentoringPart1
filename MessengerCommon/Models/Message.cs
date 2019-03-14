using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerCommon.Models
{
    public class Message
    {
        public Message()
        {
        }

        public Message(string author, string text, DateTime time)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Time = time;
        }

        public string Author { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }
    }
}
