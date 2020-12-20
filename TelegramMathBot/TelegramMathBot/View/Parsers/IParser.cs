using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramMathBot.View.Parsers
{
    public interface IParser<TIn, TOut>
    {
        TOut Parse(TIn data);
    }
}
