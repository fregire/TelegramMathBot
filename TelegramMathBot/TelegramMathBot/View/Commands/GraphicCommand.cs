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
        public GraphicCommand(ImageFormat imageFormat)
        {
            this.imageFormat = imageFormat;
        }
        public string Command => "/graph";

        public bool IsWaitingClientInput => true;

        public string HelpInfo => 
            "Команда для отрисовки графика функции по переменной x.\n";

        public string UserInputTip => "Введите функцию по переменной x";

        public IMessage GetResponse(string message)
        {
            var func = GraphicParser.Parse(message);
            var image = GraphicSolver.Solve(
                500,
                500,
                Tuple.Create(-10.0, 10.0),
                Tuple.Create(-10.0, 10.0),
                func);

            return new PhotoMessage(image, imageFormat);
        }
    }
}
