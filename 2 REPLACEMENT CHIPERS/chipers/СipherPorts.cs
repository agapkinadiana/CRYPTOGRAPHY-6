using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace chipers
{
    public static class СipherPorts
    {
        private static string alphabet;

        private static string Alphabet
        {
            get => alphabet;
            set
            {
                alphabet = value.ToLower();
                Ports = new string[Alphabet.Length, Alphabet.Length];
            }
        }

        public static string[,] Ports { get; set; }

        private static void FillArrayWithSequance(int startSequance)
        {
            for (var i = 0; i < alphabet.Length; i++)
            {
                for (var j = 0; j < alphabet.Length; j++)
                {
                    var s = startSequance.ToString();
                    Ports[i, j] = s.Insert(0, new string('0', 4 - s.Length));
                    startSequance++;
                }
            }

            //for (var i = 0; i < alphabet.Length; i++)
            //{
            //    for (var j = 0; j < alphabet.Length; j++)
            //        Console.Write(String.Format("{0}\t", Ports[i, j]));
            //    Console.WriteLine();
            //}
        }

        private static string FillIfOdd(this string str) => str.Length % 2 != 0 ? str += alphabet.ToCharArray()[alphabet.Length - 1] : str;

        private static string GetTheCipherOfAPairOfLetters(string first, string second) => Ports[alphabet.IndexOf(first, StringComparison.Ordinal), alphabet.IndexOf(second, StringComparison.Ordinal)];

        private static string GetLetter(int index) => alphabet[index].ToString();

        private static (int, int) GetPairOfLettersFromCipher(string str)
        {
            for (var i = 0; i < alphabet.Length; i++)
            {
                for (var j = 0; j < alphabet.Length; j++)
                {
                    if (Ports[i, j].Equals(str))
                        return (i, j);
                }
            }

            return (-1, -1);
        }

        public static string GetCipcherFromMessage(string alphabet, string message)
        {
            var result = new StringBuilder(message.Length);
            Alphabet = alphabet;
            FillArrayWithSequance(1);
            var messageArray = message.ToLower().FillIfOdd().ToCharArray();
            for (var i = 0; i < message.Length; i++)
            {
                result.Append(GetTheCipherOfAPairOfLetters(messageArray[i].ToString(), messageArray[i + 1].ToString()));
                i += 1;
            }

            return result.ToString();
        }

        public static string GetMessageFromCipcher(string alphabet, string cipcherMessage)
        {
            var result = new StringBuilder(cipcherMessage.Length);
            Alphabet = alphabet;
            FillArrayWithSequance(1);
            for (var i = 0; i < cipcherMessage.Length; i++)
            {
                var s = cipcherMessage.Substring(i, 4);
                var ij = GetPairOfLettersFromCipher(s);
                result.Append(GetLetter(ij.Item1) + GetLetter(ij.Item2));
                i += 3;
            }
            return result.ToString();
        }
    }
}
