using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Reflection;
using System.Text;
using TelegramMathBot.Infrastructure.MathModule;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands
{
    public class GraphicCommand : ICommand
    {
        private readonly ImageFormat imageFormat;
        private int currentCommand;
        private readonly List<Func<string, IMessage>> commands;

        public GraphicCommand(ImageFormat imageFormat)
        {
            this.imageFormat = imageFormat;
            this.commands = new List<Func<string, IMessage>>();
            InitCommands(commands);
        }
        public string Command => "/graph";

        public string HelpInfo => 
            "Команда для отрисовки графика функции по переменной x.\n";

        public ICommand CreateSameCommand()
        {
            return new GraphicCommand(imageFormat);
        }

        public (bool IsCompleted, IMessage Response) GetResponse(string message)
        {
            var nextCommand = GetNextCommand();

            return (false, nextCommand(message));
        }

        private Func<string, IMessage> GetNextCommand()
        {
            var result = commands[currentCommand];
            currentCommand = currentCommand == commands.Count - 1
                ? currentCommand : currentCommand + 1;

            return result;
        }

        private void InitCommands(List<Func<string, IMessage>> commands)
        {
            commands.Add((input) =>
            {
                return new TextMessage("Введите функцию по переменной x");
            });

            commands.Add((input) =>
            {
                var func = GraphicParser.Parse(input);
                var image = GraphicSolver.Solve(
                    500,
                    500,
                    Tuple.Create(-10.0, 10.0),
                    Tuple.Create(-10.0, 10.0),
                    func);

                return new PhotoMessage(image, imageFormat);
            });
        }
    }
}
