﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.MathModule
{
    public class DefiniteIntegral
    {
        public Func<double, double> Function { get; set; }
        public double UpperBound { get; set; }
        public double LowerBound { get; set; }
    }
}