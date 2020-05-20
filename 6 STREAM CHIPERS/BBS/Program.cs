using System;
using System.Text;

namespace BBS
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter p: ");
            long p = long.Parse(Console.ReadLine());

            Console.WriteLine("Enter q: ");
            long q = long.Parse(Console.ReadLine());

            Console.WriteLine("Enter x0: ");
            long x0 = long.Parse(Console.ReadLine());

            Console.WriteLine("Enter number of elements: ");
            int num = int.Parse(Console.ReadLine());

            BBS b = new BBS(p, q, x0);
            BBS b1 = new BBS(p, q, x0);

            Console.WriteLine("Generated num");
            for (int i = 0; i < num; i++)
            {
                Console.Write(b.getRandNum() + " ");
            }

            Console.WriteLine();

            Console.WriteLine("Generated bit");
            for (int i = 0; i < num; i++)
            {
                Console.Write(b1.getRandBit() + " ");
            }

            Console.WriteLine(); Console.WriteLine();

            //////////////////////////////

            byte[] key = { 43, 45, 100, 21, 1 };

            DateTime start1 = DateTime.Now;
            RC4 encoder = new RC4(key);
            string testString = "Agapkina Diana Sergeevna";
            Console.WriteLine("Test string: " + testString);
            byte[] testBytes = ASCIIEncoding.ASCII.GetBytes(testString);
            byte[] result = encoder.Encode(testBytes, testBytes.Length);

            string hexresult = "";
            foreach (byte bt in result)
            {
                hexresult += Convert.ToString(bt, 16);

            }
            Console.WriteLine("Encoded string: " + hexresult);
            TimeSpan procTime1 = DateTime.Now - start1;
            Console.WriteLine("Encoded time: " + procTime1.TotalSeconds.ToString() + " sec");

            DateTime start2 = DateTime.Now;
            RC4 decoder = new RC4(key);
            byte[] decryptedBytes = decoder.Decode(result, result.Length);
            string decryptedString = ASCIIEncoding.ASCII.GetString(decryptedBytes);
            Console.WriteLine("Decrypted string: " + decryptedString);
            TimeSpan procTime2 = DateTime.Now - start2;
            Console.WriteLine("Decoded time: " + procTime2.TotalSeconds.ToString() + " sec");
        }
    }
}
