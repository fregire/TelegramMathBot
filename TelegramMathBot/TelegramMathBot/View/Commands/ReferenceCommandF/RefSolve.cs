using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands.ReferenceCommandF
{
    public class RefSolve : ICommand
    {
        public string HelpInfo => throw new NotImplementedException();

        public string Command => throw new NotImplementedException();

        private readonly List<RefCategory> categories;

        public RefSolve(List<RefCategory> categories)
        {
            this.categories = categories;
        }

        public ICommand GetNextCommand()
        {
            return this;
        }

        public (ICommand NextCommand, IMessage Response) GetResponse(string message)
        {
            var isNum = int.TryParse(message, out var num);
            if (isNum)
            {
                if (IsValidIndex(num - 1, categories))
                    return (this, categories[num - 1].Message);
            }

            return (this, new TextMessage("Номер неверный"));
        }

        private bool IsValidIndex(int index, List<RefCategory> cats)
        {
            return index >= 0 && index < cats.Count;
        }
    }
}
