using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Model
{
    /// <summary>
    /// Klasa zapewnia kody służące do przechowywania i odtwarzania dowolnego stanu gry.
    /// </summary>
    public class GameSeed
    {
        static public GameSeed Generate() => new GameSeed(DateTime.Now.ToString("HHmmssf"));

        public GameSeed(string code)
        {
            Code = code;
        }

        private static readonly string base32code = "0123456789ABCDEFGHIJKLMNPRSTUWYZ";
        public string Code
        {
            get
            {
                char[] buffer = new char[6]; // 5-bit long 7 blocks for 32 bits of int
                for (int i = 0; i < 6; i++)
                {
                    int index = (intValue >> (i * 5)) & 31;
                    buffer[i] = base32code[index];
                }
                return new string(buffer);
            }
            private set
            {
                string code = value;
                intValue = 0;
                for (int i = 0, m = 1; i < code.Length && i < 6; i++, m *= 32)
                {
                    int index = base32code.IndexOf(code[i]);
                    if (index > 0) intValue += index * m;
                }
            }
        }

        public int Value { get; private set; }
        private int intValue = 0;
    }
}
