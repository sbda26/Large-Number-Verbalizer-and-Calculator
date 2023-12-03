using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace LNVC_Library
{
    public class RandomizerClass
    {
        private readonly int _maxDigits;

        public RandomizerClass(int maxDigits) => _maxDigits = maxDigits;

        public string SetNumberOfDigits(int setDigits)
        {
            if (setDigits > _maxDigits)
                throw new ArgumentOutOfRangeException($"'setDigits' cannot exceed {_maxDigits}.");
            else
                return RandomNumber(setDigits);
        }

        public string MaxDigits(int maxDigits)
        {
            if (maxDigits > _maxDigits)
                throw new ArgumentOutOfRangeException($"'setDigits' cannot exceed {_maxDigits}.");
            else
            {
                int digits = Random.Shared.Next(1, maxDigits);
                return RandomNumber(digits);
            }
        }

        public string TotallyRandomNumberOfDigits()
        {
            int digits = Random.Shared.Next(1, _maxDigits) + 1;
            return RandomNumber(digits);
        }

        private string RandomNumber(int digits)
        {
            var randomNumber = new StringBuilder(Random.Shared.Next(1, 10).ToString()); // 1->9, no leading zero;

            for (int index = 2; index <= digits; index++)
                randomNumber.Append(Random.Shared.Next(0, 10));

            return randomNumber.ToString();
        }
    }
}
