using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.View.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly List<ICommand> commands;
        public HelpCommand(List<ICommand> commands)
        {
            this.commands = commands;
        }

        public string Command => "/help";

        public bool IsWaitingClientInput => false;

        public string GetHelpText()
        {
            return "Помощь";
        }

        public string GetResponse(string message)
        {
            return "";
        }
    }
}
