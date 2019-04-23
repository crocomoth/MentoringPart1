using MessengerCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MessengerCommon.Services.Interfaces;

namespace MessengerCommon.Services
{
    public class MessageConverter : IMessageConverter
    {
        public string ConvertHistory(List<Message> messages)
        {
            // keep it local so every thread has its own. LEss time consuming, more resource consuming
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(nameof(CommandEnum.RecvHistory));
            stringBuilder.Append(":");
            foreach (var message in messages)
            {
                ConvertSingleMessage(message, stringBuilder);
                stringBuilder.Append('\x0003');
            }

            return stringBuilder.ToString();
        }

        public string ConvertMessage(Message message)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ConvertSingleMessage(message, stringBuilder);
            return stringBuilder.ToString();
        }

        public string ComposeMessage(string data)
        {
            var splitted = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(nameof(CommandEnum.Message));
            stringBuilder.Append(":");
            stringBuilder.Append(splitted[0]);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(splitted[1]);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(splitted[2]);
            return stringBuilder.ToString();
        }

        public Message CreateMessage(string data)
        {
            var splitted = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Message message = new Message(splitted[0], splitted[2], Convert.ToDateTime(splitted[1]));
            return message;
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
