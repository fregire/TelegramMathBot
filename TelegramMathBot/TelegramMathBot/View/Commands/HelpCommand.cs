using System;
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

        public string FullDescription => "Помощь";

        public string Description => "Подробное описание каждой команды";

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            var result = new StringBuilder("Команды бота:\n");
            result.Append("Сначала отправьте команду, затем отправьте аргумент\n");

            foreach (var command in commands)
            {
                var commandRow = 
                    String.Format("{0} - {1}\n", command.Command, command.FullDescription);
                result.Append(commandRow);
            }

            return (new UnknownCommand(), new TextMessage(result.ToString()));
        }
    }
}
