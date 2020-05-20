using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace chipers
{
    class MainClass
    {
        public static string alphabet = @"aąbcćdeęfghijklłmnńoóprsśtuwyzźż";

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var message = WorkWithFile.GetTextFromFile(@"pol.txt");
            //GetStatistics(message, alphabet);

            Console.WriteLine("ШИФР ЦЕЗАРЯ");
            Console.Write("Ключевое слово: ");
            string keyWord = @"diana";
            Console.Write("Ключ: ");
            //int key = 7;
            int key = Convert.ToInt32(Console.ReadLine());

            Caesar.createNewAlpha(keyWord, key);
            Console.WriteLine("Шифрованный алфавит: " + Caesar.getNewAlpha());

            string open = "", close = "";
            open = WorkWithFile.GetTextFromFile(@"pol.txt");
            DateTime start1 = DateTime.Now;

            close = Caesar.encrypt(open.ToLower());
            TimeSpan procTime1 = DateTime.Now - start1;
            Console.WriteLine("Зашифровка шифром Цезаря заняла:" + procTime1.TotalSeconds.ToString() + " sec");
            WorkWithFile.SetTextToFile(close, @"polces.txt");

            //var message2 = WorkWithFile.GetTextFromFile(@"polces.txt");
            //GetStatistics(message2, alphabet);

            DateTime start2 = DateTime.Now;
            open = Caesar.decrypt(close);
            TimeSpan procTime2 = DateTime.Now - start2;
            Console.WriteLine("Расшифровка шифром Цезаря заняла:" + procTime2.TotalSeconds.ToString() + " sec");

            Console.WriteLine("ШИФР ПОРТЫ");
            Console.WriteLine("Шифруем в файл polch.txt");
            DateTime start3 = DateTime.Now;
            var cipherPorts = СipherPorts.GetCipcherFromMessage(alphabet, message);
            TimeSpan procTime3 = DateTime.Now - start3;
            //Console.WriteLine(cipherPorts);
            Console.WriteLine("Зашифровка шифром Порты заняла:" + procTime3.TotalSeconds.ToString() + " sec");
            WorkWithFile.SetTextToFile(cipherPorts, @"polch.txt");

            var cipherMessage = WorkWithFile.GetTextFromFile(@"polch.txt");

            DateTime start4 = DateTime.Now;
            var messageP = СipherPorts.GetMessageFromCipcher(alphabet, cipherMessage);
            TimeSpan procTime4 = DateTime.Now - start4;
            Console.WriteLine("Расшифровываем");
            Console.WriteLine("Расшифровка шифром Порты заняла:" + procTime3.TotalSeconds.ToString() + " sec");
            Console.ReadKey();
        }

        public static void GetStatistics(string str, string alphabet)
        {
            double _theAlphabet = 0;
            Console.WriteLine($"Количество символов {str.Length}");
            var countLetter = new int[alphabet.Length];
            var probabilityLetters = new double[alphabet.Length];
            for (var j = 0; j < alphabet.Length; j++)
            {
                countLetter[j] = str.Count(x => x == alphabet[j]);
                var jCount = str.Count(x => x == alphabet[j]);
                //Console.WriteLine($"{alphabet[j]}: {countLetter[j]}");

                probabilityLetters[j] = (double)countLetter[j] / str.Length;
                Console.WriteLine($"G({alphabet[j]}): {probabilityLetters[j]}");

                _theAlphabet += probabilityLetters[j] == 0 ? 0 : probabilityLetters[j] * (Math.Log(probabilityLetters[j], 2)) * (-1);
            }

            //Console.WriteLine(probabilityLetters.Sum());
            Console.WriteLine($"Энтропия - {_theAlphabet}");
        }
    }
}
