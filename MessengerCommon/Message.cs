using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerCommon
{
    public class Message
    {
        public Message()
        {
        }

        public Message(string author, string text)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Author { get; set; }

        public string Text { get; set; }
    }
}
