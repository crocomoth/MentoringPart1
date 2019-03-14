using MessengerCommon.Models;
using System.Collections.Generic;
using System.Linq;

namespace MessengerServer.Services
{
    public class MessageQueue
    {
        private LinkedList<Message> messages;
        private int limit;
        private object lockObj;

        public MessageQueue(int size = 100)
        {
            this.messages = new LinkedList<Message>();
            this.limit = size;
        }

        public void AddToQueue(Message message)
        {
            lock (lockObj)
            {
                if (this.messages.Count > limit)
                {
                    this.messages.RemoveFirst();
                }

                this.messages.AddLast(message);
            }
        }

        public Message Dequeue()
        {
            lock (lockObj)
            {
                var result = this.messages.First;
                this.messages.RemoveFirst();

                return result.Value;
            }
        }

        public List<Message> GetAllMessages()
        {
            lock (lockObj)
            {
                return this.messages.ToList();
            }
        }
    }
}
