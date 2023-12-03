using System;
using System.IO;
using System.Numerics;

namespace LNVC_Library
{
    public class CalculatorClass
    {
        public string? Calc1Operand(string operand1Str, string? symbol, string decimalChar)
        {
            if (string.IsNullOrWhiteSpace(operand1Str) == false && operand1Str.Contains(decimalChar) == false)
            {
                var operand1 = BigInteger.Parse(operand1Str);
                string? result = Compute(operand1, symbol);
                return result;
            }
            else
                throw new InvalidDataException("[Operand1] cannot be blank or contain a decimal.");
        }

        public string? Calc2Operands(string? operand1Str, string? operand2Str, string? symbol, string decimalChar)
        {
            if (!string.IsNullOrWhiteSpace(operand1Str) && !string.IsNullOrWhiteSpace(operand2Str))
            {
                if (operand1Str.Contains(decimalChar) == false && operand2Str.Contains(decimalChar) == false)
                {
                    var operand1 = BigInteger.Parse(operand1Str);
                    var operand2 = BigInteger.Parse(operand2Str);
                    string? result = Compute(operand1, operand2, symbol);
                    return result;
                }
                else
                {
                    var operand1 = Double.Parse(operand1Str);
                    var operand2 = Double.Parse(operand2Str);
                    string? result = Compute(operand1, operand2, symbol);
                    return result;
                }
            }
            else
                throw new InvalidDataException("One of the operands is blank.");
        }

// ==================================================================================================================================================================================================

        private string? Compute(BigInteger operand1, string? symbol)
        {
            BigInteger result = symbol switch
            {
                "Σ" => Sum_Gauss(operand1),
                "!" => Factorial(operand1),
                _ => throw new Exception($"Invalid symbol: {symbol}."),
            };
            return result.ToString();
        }

        private string? Compute(BigInteger operand1, BigInteger operand2, string? symbol)
        {
            BigInteger result = symbol switch
            {
                "+" => operand1 + operand2,
                "-" => operand1 - operand2,
                "X" => operand1 * operand2,
                "/" => operand1 / operand2,
                "x^y" => BigInteger.Pow(operand1, (int)operand2),
                "AND" => operand1 & operand2,
                "OR" => operand1 | operand2,
                "XOR" => operand1 ^ operand2,
                "MOD" => operand1 % operand2,
                "MED" => Median(operand1, operand2),
                "SHL" => operand1 << (int)operand2,
                "SHR" => operand1 >> (int)operand2,
                _ => throw new Exception($"Invalid symbol: {symbol}."),
            };
            return result.ToString();
        }

        private string? Compute(double operand1, double operand2, string? symbol)
        {
            double result = symbol switch
            {
                "+" => operand1 + operand2,
                "-" => operand1 - operand2,
                "X" => operand1 * operand2,
                "/" => operand1 / operand2,
                "x^y" => Double.Pow(operand1, operand2),
                "MED" => Median(operand1, operand2),
                _ => throw new InvalidOperationException($"Invalid symbol or unable to perform binary operation {symbol} on a floating-point number."),
            };
            return result.ToString();
        }

        private BigInteger Factorial(BigInteger operand)
        {
            BigInteger result = operand;

            for (BigInteger index = (operand - 1); index > 1; index--)
                result *= index;

            return result;
        }

        public string? GetExponentialNotation(string number, string decimalChar)
        {
            if (string.IsNullOrWhiteSpace(number) == true || number == "-")
                return null;
            else
            {
                string raw = number.Contains(decimalChar)
                    ? double.Parse(number).ToString("E2")
                    : BigInteger.Parse(number).ToString("E2");

                int indexE = raw.IndexOf("E");
                decimal baseValue = decimal.Parse(raw[0..indexE]);
                int exponent = int.Parse(raw[(indexE + 2)..]);
                string returnValue = $"{baseValue:F2} X 10^{exponent}";

                return returnValue;
            }
        }

        private double Median(double v1, double v2)
        {
            if (v1 == v2)
                return v1;
            else
            {
                if (v1 > v2)
                    (v1, v2) = (v2, v1);

                return v1 + ((v2 - v1) / 2);
            }
        }

        private BigInteger Median(BigInteger v1, BigInteger v2)
        {
            if (v1 == v2)
                return v1;
            else
            {
                if (v1 > v2)
                    (v1, v2) = (v2, v1);

                return v1 + ((v2 - v1) / 2);
            }
        }

        private BigInteger Sum_Gauss(BigInteger operand1)
        {
            BigInteger result = 0;

            for (BigInteger index = operand1; index > 0; index--)
                result += index;

            return result;
        }
    }
}
