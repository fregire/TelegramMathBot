using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using TelegramMathBot.View.Commands.ReferenceCommandF;

namespace TelegramMathBot.View.Commands
{
    public static class CommandsFactory
    {
        public static ExpressionCommand CreateExpressionCommand()
        {
            return new ExpressionCommand();
        }

        public static GraphicCommand CreateGraphicCommand(ImageFormat imageFormat)
        {
            return new GraphicCommand(imageFormat);
        }

        public static HelpCommand CreateHelpCommand(List<ICommand> commands)
        {
            return new HelpCommand(commands);
        }

        public static ReferenceCommand CreateReferenceCommand()
        {
            return new ReferenceCommand();
        }
    }
}
