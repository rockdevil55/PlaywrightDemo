using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomString4Net;
using System.Threading.Tasks;

namespace PlaywrightDemo.utils
{
    public class GenericMethod
    {
        private static readonly Random RandomGenerator = new Random();

        public int GetRandomNumber(int min = 0, int max = int.MaxValue)
        {
            return RandomGenerator.Next(min, max);
        }

        public string GetRandomString(int length = 15)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[RandomGenerator.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());

            return s;
        }

    }
}
