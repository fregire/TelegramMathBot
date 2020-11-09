using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.Domain
{
    public enum RequestType
    {
        None,
        WaitingForResult,
        Expression,
        Help,
        Graphic
    }
}
