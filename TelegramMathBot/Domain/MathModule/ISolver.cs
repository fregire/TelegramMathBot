using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.MathModule
{
    public interface ISolver<TIn, TOut>
    {
        TOut Solve(TIn inputData);
    }
}
