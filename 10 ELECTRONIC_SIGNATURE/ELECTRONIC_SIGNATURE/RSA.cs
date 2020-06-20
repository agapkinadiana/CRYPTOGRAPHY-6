using System;
using System.Security.Cryptography;
using System.Text;
using System.Numerics;
using System.IO;
using System.Collections.Generic;

namespace ELECTRONIC_SIGNATURE
{
    public class RSA
    {
        public static void CheckCorrectly()
        {
            var p = 7;
            var q = 13;
            var pathToSource = "Source.txt";
            var pathToEds = "RSA.txt";
            var result = Create(p, q, pathToSource, pathToEds);
            var veryify = Verify(result.d, result.n, pathToEds, pathToSource);
            Console.WriteLine("Проверка на корректность RSA - " + veryify);
        }

        public static void CheckInCorrectly()
        {
            var p = 7;
            var q = 13;

            var pathToSource = "Source.txt";
            var pathToFakeSource = "FakeSource.txt";
            var pathToEds = "RSA.txt";
            DateTime start1 = DateTime.Now;
            var result = Create(p, q, pathToSource, pathToEds);
            var veryify = Verify(result.d, result.n, pathToEds, pathToFakeSource);
            Console.WriteLine("Электронно цифровая подпись на основе RSA действительна? - " + veryify);
            TimeSpan procTime1 = DateTime.Now - start1;
            Console.WriteLine("Потраченное время на проверку: " + procTime1.TotalSeconds.ToString() + " sec");
            Console.WriteLine("___________________________________________________________________________");
        }

        private static readonly char[] Characters = { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };

        private static (long d, long n) Create(long p, long q, string sourceFilePathTextBox, string signFilePathTextBox)
        {
            var hash = File.ReadAllText(sourceFilePathTextBox).GetHashCode().ToString();
            var n = p * q;
            var m = (p - 1) * (q - 1);
            var d = Calculate_d(m);
            var e = Calculate_e(d, m);

            var result = RSA_Encode(hash, e, n);

            var sw = new StreamWriter(signFilePathTextBox);
            foreach (var item in result)
            {
                sw.WriteLine(item);
            }
            sw.Close();

            return (d, n);
        }

        private static bool Verify(long d, long n, string signFilePathTextBox, string sourceFilePathTextBox)
        {
            var input = new List<string>();

            var sr = new StreamReader(signFilePathTextBox);

            while (!sr.EndOfStream)
            {
                input.Add(sr.ReadLine());
            }

            sr.Close();

            var result = RSA_Decode(input, d, n);

            var hash = File.ReadAllText(sourceFilePathTextBox).GetHashCode().ToString();

            if (result.Equals(hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<string> RSA_Encode(string s, long e, long n)
        {
            var result = new List<string>();

            foreach (var t in s)
            {
                var index = Array.IndexOf(Characters, t);
                var bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);
                var bn = new BigInteger((int)n);
                bi %= bn;
                result.Add(bi.ToString());
            }

            return result;
        }

        private static string RSA_Decode(List<string> input, long d, long n)
        {
            var result = "";
            var bn = new BigInteger((int)n);
            foreach (var item in input)
            {
                var bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);
                bi = bi % bn;

                var index = Convert.ToInt32(bi.ToString());

                result += Characters[index].ToString();
            }

            return result;
        }

        private static long Calculate_d(long m)
        {
            var d = m - 1;

            for (long i = 2; i <= m; i++)
            {
                if (m % i == 0 && d % i == 0)
                {
                    d--;
                    i = 1;
                }
            }

            return d;
        }

        private static long Calculate_e(long d, long m)
        {
            long e = 10;

            while (true)
            {
                if (e * d % m == 1)
                {
                    break;
                }
                else
                {
                    e++;
                }
            }
            return e;
        }
    }
}
