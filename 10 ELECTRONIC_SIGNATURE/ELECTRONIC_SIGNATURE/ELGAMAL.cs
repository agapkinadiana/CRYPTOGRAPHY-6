using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace ELECTRONIC_SIGNATURE
{
    public class ELGAMAL
    {
        public static void CheckCorrectly()
        {
            var str = "Hello world";
            var hash = CalculateMd5Hash(str).ToString();
            var sign = ElGamalClass.EnCrypt(hash);
            var verify = ElGamalClass.DeCrypt(sign) == CalculateMd5Hash(str).ToString();
            Console.WriteLine("Проверка на корректность ElGamal - " + verify);
        }
        public static void CheckInCorrectly()
        {
            var str = "Hello world";
            var fakeStr = "Hello world";
            DateTime start1 = DateTime.Now;
            var hash = CalculateMd5Hash(str).ToString();
            var sign = ElGamalClass.EnCrypt(hash);
            var verify = ElGamalClass.DeCrypt(sign) == CalculateMd5Hash(fakeStr).ToString();
            Console.WriteLine("Электронно цифровая подпись на основе ElGamal действительна? - " + verify);
            TimeSpan procTime1 = DateTime.Now - start1;
            Console.WriteLine("Потраченное время на проверку: " + procTime1.TotalSeconds.ToString() + " sec");
            Console.WriteLine("___________________________________________________________________________");

        }
        private static BigInteger CalculateMd5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            return new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
        }
    }

    public static class ElGamalClass
    {
        private static int Power(int a, int b, int n)
        {
            var tmp = a;
            var sum = tmp;
            for (var i = 1; i < b; i++)
            {
                for (var j = 1; j < a; j++)
                {
                    sum += tmp;
                    if (sum >= n)
                    {
                        sum -= n;
                    }
                }

                tmp = sum;
            }

            return tmp;
        }

        private static int Mul(int a, int b, int n)
        {
            var sum = 0;

            for (var i = 0; i < b; i++)
            {
                sum += a;

                if (sum >= n)
                {
                    sum -= n;
                }
            }

            return sum;
        }

        public static string EnCrypt(string str)
        {
            return Crypt(593, 123, 8, str);
        }

        public static string DeCrypt(string str)
        {
            return Decrypt(593, 8, str);
        }


        /*****************************************************
        p - простое число
        0 < g < p-1
        0 < x < p-1
        m - шифруемое сообщение m < p
        *****************************************************/
        // 593, 123, 8
        private static string Crypt(int p, int g, int x, string inString)
        {
            var result = "";
            var y = Power(g, x, p);
            var rand = new Random();
            foreach (int code in inString)
                if (code > 0)
                {
                    var k = rand.Next() % (p - 2) + 1; // 1 < k < (p-1) 
                    var a = Power(g, k, p);
                    var b = Mul(Power(y, k, p), code, p);
                    result += a + " " + b + " ";
                }

            return result;
        }

        private static string Decrypt(int p, int x, string inText)
        {
            var result = "";

            var arr = inText.Split(' ').Where(xx => xx != "").ToArray();
            for (var i = 0; i < arr.Length; i += 2)
            {
                var a = int.Parse(arr[i]);
                var b = int.Parse(arr[i + 1]);

                if (a != 0 && b != 0)
                {
                    //wcout<<a<<" "<<b<<endl; 

                    var deM = Mul(b, Power(a, p - 1 - x, p),
                        p); // m=b*(a^x)^(-1)mod p =b*a^(p-1-x)mod p - трудно было  найти нормальную формулу, в ней вся загвоздка 
                    var m = (char)deM;
                    result += m;
                }
            }

            return result;
        }
    }
}
