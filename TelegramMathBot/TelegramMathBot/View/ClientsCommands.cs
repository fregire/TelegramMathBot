using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.App;

namespace TelegramMathBot.View
{
    public class ClientsCommands
    {
        private readonly ClientManager clientManager;
        public ClientsCommands(ClientManager clientManager)
        {
            this.clientManager = clientManager;
        }


    }
}
