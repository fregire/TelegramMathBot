﻿using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

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

        public string UserInputTip => null;

        public string HelpInfo => "Помощь";

        public IMessage GetResponse(string message)
        {
            var result = new StringBuilder("Команды бота:\n");
            result.Append("Сначала отправьте команду, затем отправьте аргумент\n");

            foreach (var command in commands)
            {
                var commandRow = 
                    String.Format("{0} - {1}\n", command.Command, command.HelpInfo);
                result.Append(commandRow);
            }

            return new TextMessage(result.ToString());
        }
    }
}
