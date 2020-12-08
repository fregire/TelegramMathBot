using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands.GraphicCommand
{
    public class GraphicHelp : ICommand
    {
        public string HelpInfo => "Команда для отрисовки графика функции по переменной x.";

        public string Command => "/graph";
        private readonly ImageFormat imageFormat;

        public GraphicHelp(ImageFormat imageFormat)
        {
            this.imageFormat = imageFormat;
        }

        public ICommand GetNextCommand()
        {
            return new GraphicSolve(imageFormat);
        }

        public IMessage GetResponse(string message)
        {
            return new TextMessage("Введите функцию по переменной x");
        }
    }
}
