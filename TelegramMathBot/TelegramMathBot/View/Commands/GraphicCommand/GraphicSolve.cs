﻿using Domain.MathModule;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using TelegramMathBot.View.Messages;
using TelegramMathBot.View.Parsers;

namespace TelegramMathBot.View.Commands.GraphicCommand
{
    public class GraphicSolve : ICommand
    {
        public string HelpInfo => throw new NotImplementedException();

        public string Command => throw new NotImplementedException();

        private readonly ImageFormat imageFormat;

        public GraphicSolve(ImageFormat imageFormat)
        {
            this.imageFormat = imageFormat;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            try
            {
                var func = GraphicParser.Parse(message);
                var image = GraphicSolver.Solve(
                    500,
                    500,
                    Tuple.Create(-10.0, 10.0),
                    Tuple.Create(-10.0, 10.0),
                    func);

                return (this, new PhotoMessage(image, imageFormat));
            }
            catch
            {
                return (this, new TextMessage("Функция неверная"));
            }
        }
    }
}
