using MessengerCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessengerServer.Services
{
    public class MessageConverter
    {
        public string ConvertHistory(List<Message> messages)
        {
            // keep it local so every thread has its own. LEss time consuming, more resource consuming
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var message in messages)
            {
                ConvertSingleMessage(message, stringBuilder);
                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        public string ConvertMessage(Message message)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ConvertSingleMessage(message, stringBuilder);
            return stringBuilder.ToString();
        }

        private void ConvertSingleMessage(Message message, StringBuilder stringBuilder)
        {
            stringBuilder.Append(nameof(CommandEnum.Message));
            stringBuilder.Append(":");
            stringBuilder.Append(message.Author);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(message.Time);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(message.Text);
        }
    }
}
