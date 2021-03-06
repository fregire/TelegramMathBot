﻿using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using TelegramMathBot.Properties;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands.ReferenceCommandF
{
    public class RefHelp : ICommand
    {
        public string FullDescription => "Справка по основным формулам";
        public string Description => "Справка по основным формулам";

        public string Command => "/ref";

        private readonly List<RefCategory> categories;
        public RefHelp()
        {
            categories = GetCategories();
        }

        private List<RefCategory> GetCategories()
        {
            var result = new List<RefCategory>();
            
            result.Add(
                new RefCategory(
                    "Производная",
                    new PhotoMessage(Properties.Resources.derivative, ImageFormat.Png)));
            result.Add(
                new RefCategory(
                    "Интегралы",
                    new PhotoMessage(Properties.Resources.integral, ImageFormat.Png)));

            return result;
        }

        private string GetFormattedCatsNames(List<RefCategory> cats)
        {
            var result = new StringBuilder();

            for (var i = 0; i < cats.Count; i++)
            {
                result.Append(i + 1 + ". ");
                result.Append(cats[i].Name);
                result.Append("\n");
            }

            return result.ToString();
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            return (new RefSolve(categories), new TextMessage(GetFormattedCatsNames(categories)));
        }
    }
}
