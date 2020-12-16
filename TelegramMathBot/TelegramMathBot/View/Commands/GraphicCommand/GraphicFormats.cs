using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.ImageFormats;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands.GraphicCommand
{
    public class GraphicFormats : ICommand
    {
        public string FullDescription => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public string Command => throw new NotImplementedException();
        public readonly List<IImageFormat> imageFormats;

        public GraphicFormats(List<IImageFormat> imageFormats)
        {
            this.imageFormats = imageFormats;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            var isNum = int.TryParse(message, out var num);

            if (isNum)
            {
                var index = num - 1;

                if (IsValidIndex(index))
                    return (new GraphicSolve(imageFormats[index]), new TextMessage("Введите функцию по переменной x"));

                return (this, new TextMessage("Введите номер из предложенного списка"));
            }

            return (this, new TextMessage("Введите число"));
        }

        private bool IsValidIndex(int index)
        {
            return index < imageFormats.Count && index > -1;
        }
    }
}
