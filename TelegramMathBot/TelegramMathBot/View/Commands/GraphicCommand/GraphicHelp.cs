using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands.GraphicCommand
{
    public class GraphicHelp : ICommand
    {
        public string FullDescription => "Команда для отрисовки графика функции по переменной x.";

        public string Command => "/graph";

        public string Description => "Отрисовка графика функции";

        private readonly ImageFormat imageFormat;

        public GraphicHelp(ImageFormat imageFormat)
        {
            this.imageFormat = imageFormat;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            return (new GraphicSolve(imageFormat), new TextMessage("Введите функцию по переменной x"));
        }
    }
}
