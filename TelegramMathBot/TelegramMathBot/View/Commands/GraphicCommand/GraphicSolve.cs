using Domain.MathModule;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using TelegramMathBot.View.ImageFormats;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands.GraphicCommand
{
    public class GraphicSolve : ICommand
    {
        public string FullDescription => throw new NotImplementedException();

        public string Command => throw new NotImplementedException();
        public string Description => throw new NotImplementedException();

        private readonly IImageFormat imageFormat;

        //Допустим хотим сделать свой формат(экзотический, ASCII), 
        //который возвращает данные в разном формате(в виде картинки, текста(ascii))  
        // => нужен интерфейс 
        // интерфейс принимает Image и возвращает в своем формате

        public GraphicSolve(IImageFormat imageFormat)
        {
            this.imageFormat = imageFormat;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var func = FunctionParser.Parse(message);
                var image = GraphicSolver.Solve(
                    500,
                    500,
                    Tuple.Create(-10.0, 10.0),
                    Tuple.Create(-10.0, 10.0),
                    func);

                return (this, imageFormat.GetResult(image));
            }
            catch
            {
                return (this, new TextMessage("Функция неверная"));
            }
        }
    }
}
