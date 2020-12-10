using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using TelegramMathBot.View.Commands;
using TelegramMathBot.View.Commands.ExpressionCommand;
using TelegramMathBot.View.Commands.GraphicCommand;
using TelegramMathBot.View.Commands.IntegralCommand;
using TelegramMathBot.View.Commands.ReferenceCommandF;

namespace TelegramMathBot.View.Commands
{
    public static class CommandsFactory
    {
        public static ExpressionHelp CreateExpressionCommand()
        {
            return new ExpressionHelp();
        }

        public static GraphicHelp CreateGraphicCommand(ImageFormat imageFormat)
        {
            return new GraphicHelp(imageFormat);
        }

        public static HelpCommand CreateHelpCommand(List<ICommand> commands)
        {
            return new HelpCommand(commands);
        }

        public static RefHelp CreateReferenceCommand()
        {
            return new RefHelp();
        }

        public static StartCommand CreateStartCommand()
        {
            return new StartCommand();
        }

        public static IntegralHelp CreateIntegralCommand()
        {
            return new IntegralHelp();
        }
    }
}
