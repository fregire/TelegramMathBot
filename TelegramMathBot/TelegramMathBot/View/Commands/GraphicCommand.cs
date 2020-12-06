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
    public class GraphicCommand : StepByStepCommand
    {
        private readonly ImageFormat imageFormat;

        public GraphicCommand(ImageFormat imageFormat) : base()
        {
            this.imageFormat = imageFormat;
        }

        public override string Command => "/graph";
        public override string HelpInfo => "Команда для отрисовки графика функции по переменной x.";

        public override ICommand CreateSameCommand()
        {
            return new GraphicCommand(imageFormat);
        }

        protected override List<Func<string, IMessage>> GetInitedCommands()
        {
            var result = new List<Func<string, IMessage>>();

            result.Add((input) =>
            {
                return new TextMessage("Введите функцию по переменной x");
            });

            result.Add((input) =>
            {
                try
                {
                    var func = GraphicParser.Parse(input);
                    var image = GraphicSolver.Solve(
                        500,
                        500,
                        Tuple.Create(-10.0, 10.0),
                        Tuple.Create(-10.0, 10.0),
                        func);

                    return new PhotoMessage(image, imageFormat);
                }
                catch
                {
                    return new TextMessage("Функция неверная");
                }
            });

            return result;
        }

        protected override (bool IsCompleted, Func<string, IMessage> command) GetNextCommand()
        {
            var result = commands[currentCommand];
            currentCommand = currentCommand == commands.Count - 1
                ? currentCommand : currentCommand + 1;

            return (false, result);
        }
    }
}
