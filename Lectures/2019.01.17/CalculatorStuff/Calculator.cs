using System;
using System.Text.RegularExpressions;

namespace CalculatorStuff
{
    public class Calculator
    {
        const string Left = "Left";
        const string Operator = "Operator";
        const string Right = "Right";
        public const string EquationPattern =
            @"\s*(?<"+Left+@">\d+)\s*(?<"+Operator+@">\+|-)\s*(?<"+Right+@">\d+)\s*";

        public Regex EquationRegEx { get; } = new Regex(EquationPattern);

        public int Calculate(string equation)
        {
            if(equation is null)
            {
                throw new ArgumentNullException(
                    nameof(equation));
            }
            (int left, char op, int right) = Parse(equation);
            
            switch(op)
            {
                case '+':
                    return left + right;
                case '-':
                    return left - right;
            }

            throw new Exception($"Operation ({op}) not surpported.");
            
        }

        private (int left, char op, int right) Parse(
            string equation)
        {
            Match match = EquationRegEx.Match(equation);
            if(!match.Success)
            {
                throw new ArgumentException(equation,
                    "The equation could not be evaluated.");
            }

            return (
                int.Parse(match.Groups[Left].Value),        
                char.Parse(match.Groups[Operator].Value),
                int.Parse(match.Groups[Right].Value)
                );
        }
    }
}
