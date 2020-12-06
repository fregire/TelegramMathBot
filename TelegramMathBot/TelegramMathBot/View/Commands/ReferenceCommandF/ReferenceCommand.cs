using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands.ReferenceCommandF
{
    public class ReferenceCommand : StepByStepCommand
    {
        public override string HelpInfo => "Справка по основным формулам";

        public override string Command => "/ref";
        private readonly List<RefCategory> categories;
        public ReferenceCommand(): base()
        {
            categories = GetCategories();
        }

        public override ICommand CreateSameCommand()
        {
            return new ReferenceCommand();
        }

        private List<RefCategory> GetCategories()
        {
            var result = new List<RefCategory>();
            result.Add(
                new RefCategory(
                    "Производная", 
                    new PhotoMessage(new Image("Images/derivative.png"), ImageFormat.Png)));

            return result;
        }

        private string GetFormattedCatsNames(List<RefCategory> cats)
        {
            var result = new StringBuilder();
            
            for(var i = 0; i < cats.Count; i++)
            {
                result.Append(i + 1 + ". ");
                result.Append(cats[i].Name);
                result.Append("\n");
            }

            return result.ToString();
        }

        private bool IsValidIndex(int index, List<RefCategory> cats)
        {
            return index >= 0 && index < cats.Count;
        }

        protected override List<Func<string, IMessage>> GetInitedCommands()
        {
            var result = new List<Func<string, IMessage>>();

            result.Add((input) =>
            {
                return new TextMessage(GetFormattedCatsNames(categories));
            });

            result.Add((input) =>
            {
                var isNum = int.TryParse(input, out var num);
                if (isNum)
                {
                    if (IsValidIndex(num - 1, categories))
                        return categories[num - 1].Message;
                }

                return new TextMessage("Номер неверный");
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
