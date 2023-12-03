using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNVC_Library
{
    public class VerbalizeClass
    {
        private const string _numberFile = "Very_Large_Numbers.txt";
        private const string _orConjunction = " or ";

        private readonly string _decimalChar;

        public List<VerbalizedNumbersClass> Illions { get; private set; } = new();

        public VerbalizeClass(string decimalChar)
        {
            _decimalChar = decimalChar;
            AddToIllions(string.Empty, 0);
            using (var sr = new StreamReader(_numberFile))
            {
                while (sr.EndOfStream == false)
                {
                    string? line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line) == false)
                    {
                        string[] values = line.Split('\t');
                        if (values.Length == 2)
                        {
                            if (int.TryParse(values[1], out int power) == true)
                            {
                                if ((power % 3) == 0)
                                {
                                    string name = NameWithoutOr(values[0]);
                                    AddToIllions(name, power);
                                }
                            }
                        }
                    }
                }
                sr.Close();
            }
        }

        public string VerbalizedName(string numericValue)
        {
            int decimalCharIndex = numericValue.IndexOf(_decimalChar);

            if (decimalCharIndex == -1)
                return VerbalizedName_Integer(numericValue);
            else
                return VerbalizedName_FloatingPoint(numericValue, decimalCharIndex);
        }

// ====================================================================================================================================================================================================================================

        private void AddToIllions(string name, int power) =>
            Illions.Add(new VerbalizedNumbersClass
            {
                Name = name,
                Power = power,
                MinLength = power + 1,
                MaxLength = power + 3
            });

        private string NameWithoutOr(string name)
        {
            int orConjunctionIndex = name.IndexOf(_orConjunction);

            if (orConjunctionIndex > -1)
                name = name[0..orConjunctionIndex];

            return name;
        }

        private string VerbalizedName_FloatingPoint(string numericValue, int decimalCharIndex) =>
            $"{VerbalizedName_Integer(numericValue[0..decimalCharIndex])}{_decimalChar}{numericValue[(decimalCharIndex + 1)..]}";

        private string VerbalizedName_Integer(string numericValue)
        {
            var result = new StringBuilder();

            if (numericValue.Length % 3 == 1)
                numericValue = $"00{numericValue}";
            else if (numericValue.Length % 3 == 2)
                numericValue = $"0{numericValue}";

            int illionsIndex = (numericValue.Length / 3) - 1;

            for (int integerValueIndex = 0; integerValueIndex < numericValue.Length; integerValueIndex += 3)
            {
                string fragmentValue = numericValue.Substring(integerValueIndex, 3);
                while (fragmentValue.StartsWith("0") == true)
                    fragmentValue = fragmentValue[1..];
                if (string.IsNullOrEmpty(fragmentValue) == false)
                    result.Append($"{fragmentValue} {Illions[illionsIndex].Name} ");
                illionsIndex--;
            };

            return result.ToString().TrimEnd();
        }
    }
}
