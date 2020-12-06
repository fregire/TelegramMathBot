using System;
using System.Collections.Generic;
using System.Text;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View.Commands
{
    public abstract class StepByStepCommand : ICommand
    {
        private protected int currentCommand;
        private protected readonly List<Func<string, IMessage>> commands;
        public StepByStepCommand()
        {
            this.commands = GetInitedCommands();
        }

        public (bool IsCompleted, IMessage Response) GetResponse(string input)
        {
            (var isCompleted, var nextCommand) = GetNextCommand();

            return (isCompleted, nextCommand(input));
        }

        protected abstract List<Func<string, IMessage>> GetInitedCommands();
        protected abstract (bool IsCompleted, Func<string, IMessage> command) GetNextCommand();
        public abstract string HelpInfo { get; }
        public abstract string Command { get; }
        public abstract ICommand CreateSameCommand();
    }
}
