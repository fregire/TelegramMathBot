using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.Domain
{
    public class Command
    {
        public RequestType RequestType { get; }
        public bool IsWaitingForResponse { get; }
        public Command(
            RequestType requestType, 
            bool waitingForResponse, 
            Func<string, string> getResponse=null,
            string commandName=null,
            string tip=null)
        {
            this.RequestType = requestType;
            this.IsWaitingForResponse = waitingForResponse;
        }

    }
}
