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
    public class GraphicHelp : ICommand
    {
        public string FullDescription => "Команда для отрисовки графика функции по переменной x.";

        public string Command => "/graph";

        public string Description => "Отрисовка графика функции";

        private readonly List<IImageFormat> imageFormats;
        private readonly ISolver<GraphicConfig, Image> graphicSolver;
        private readonly IParser<string, Func<double, double>> funcParser;
        public GraphicHelp(
            ISolver<GraphicConfig, Image> graphicSolver,
            IParser<string, Func<double, double>> funcParser,
            List<IImageFormat> imageFormats)
        {
            this.imageFormats = imageFormats;
            this.graphicSolver = graphicSolver;
            this.funcParser = funcParser;
        }

        public string GetFormattedListFormats()
        {
            var result = new StringBuilder();

            for(var i = 0; i < imageFormats.Count; i++)
                result.Append($"{i + 1} {imageFormats[i].Name}\n");

            return result.ToString();
        }

        public string GetText()
        {
            return "Выберите подходящий формат:\n" + GetFormattedListFormats();
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            return (
                new GraphicFormats(graphicSolver, funcParser, imageFormats), 
                new TextMessage(GetText()));
        }
    }
}
