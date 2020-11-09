using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TelegramMathBot.View.Parsers
{
    public class PolynomParser
    {
        private static List<(double Deg, double Coeff)> GetOrderedCoeffAndDegree(MatchCollection matches)
        {
            var coeffs = new List<(double Deg, double Coeff)>();
            foreach (Match match in matches)
            {
                var isCoeff = double.TryParse(match.Groups[2].Value, out var coeff);
                var isCoeffNegative = match.Groups[1].Value == "-";
                if (isCoeffNegative && isCoeff)
                    coeff = -coeff;

                var isDeg = double.TryParse(match.Groups[3].Value, out var deg);
                var isNum = int.TryParse(match.Value, out var num);
                if (isNum)
                    coeffs.Add((0, num));
                else
                {
                    if (isCoeff)
                    {
                        if (isDeg)
                            coeffs.Add((deg, coeff));
                        else
                            coeffs.Add((1, coeff));
                    }
                    else
                    {
                        if (isDeg)
                            coeffs.Add((deg, 1));
                        else
                            coeffs.Add((1, 1));
                    }
                }
            }

            return coeffs
                .OrderByDescending(pair => pair.Deg)
                .ToList();
        }

        public static List<double> Parse(string input)
        {
            input = input.Replace(" ", string.Empty);
            var regex = new Regex(@"([-]*)([0-9]*)[x0-9][\^]*([0-9]*)");
            var matches = regex.Matches(input);
            var coeffs = GetOrderedCoeffAndDegree(matches);
            var result = new List<double>();


            for (var i = 0; i < coeffs.Count - 1; i++)
            {
                if (coeffs[i].Deg == coeffs[i + 1].Deg)
                    coeffs[i + 1] = (coeffs[i].Deg, coeffs[i].Coeff + coeffs[i + 1].Coeff);
                else
                {
                    var diff = coeffs[i].Deg - coeffs[i + 1].Deg;
                    result.Add(coeffs[i].Coeff);

                    for (var j = 0; j < diff - 1; j++)
                        result.Add(0);
                }
            }

            result.Add(coeffs[coeffs.Count - 1].Coeff);

            if (coeffs.Count == 1)
            {
                for (var i = 0; i < coeffs[0].Deg; i++)
                    result.Add(0);
            }

            return result;
        }
    }
}
