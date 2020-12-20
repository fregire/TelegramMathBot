using Domain.MathModule;
using Domain.MathModule.Graphic;
using SFML.Graphics;
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
        private readonly GraphicSolver solver;
        private readonly FunctionParser parser;

        //Допустим хотим сделать свой формат(экзотический, ASCII), 
        //который возвращает данные в разном формате(в виде картинки, текста(ascii))  
        // => нужен интерфейс 
        // интерфейс принимает Image и возвращает в своем формате

        public GraphicSolve(
            GraphicSolver solver, 
            FunctionParser parser,
            IImageFormat imageFormat)
        {
            this.imageFormat = imageFormat;
            this.solver = solver;
            this.parser = parser;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var func = parser.Parse(message);
                var config = new GraphicConfig(
                    new System.Drawing.Size(500, 500),
                    Tuple.Create(-10.0, 10.0),
                    Tuple.Create(-10.0, 10.0),
                    func);
                var image = solver.Solve(config);

                return (this, imageFormat.GetResult(image));
            }
            catch
            {
                return (this, new TextMessage("Функция неверная"));
            }
        }
    }
}
