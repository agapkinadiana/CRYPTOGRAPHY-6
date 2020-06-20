using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Numerics;
namespace KMZI13
{
    /// <summary>
    /// Логика взаимодействия для Encrypt.xaml
    /// </summary>
    public partial class Encrypt : Window
    {
        int mod(int k, int n) { return ((k %= n) < 0) ? k + n : k; }
        Dictionary<char, (int x,int y)> hash = new Dictionary<char, (int x,int y)>();
        Dictionary<(int x, int y),char> obrhash = new Dictionary<(int x, int y),char>();
        int p = 751;
        int d = 29;
        public Encrypt()
        {
            InitializeComponent();
        }
        string SumTwoPoints(int xP, int xQ, int yP, int yQ)
        {
            BigInteger lyambda;
            int raznX = xQ - xP;
            int raznY = yQ - yP;
            if (raznX < 0)
            {
                raznX += p;
            }
            if (raznY < 0)
            {
                raznY += p;
            }
            if (xP == 0 & yP == 0)
            {
                return xQ.ToString() + ',' + yQ.ToString();
            }
            if (xQ == 0 & yQ == 0)
            {
                return xP.ToString() + ',' + yP.ToString();
            }
            BigInteger xR = 0, yR = 0;
            if (xP == xQ && yP != yQ || (yP == 0 && yQ == 0 && xP == xQ))
            { }
            else
            {
                if (xP == xQ && yP == yQ)
                {
                    lyambda = (3 * BigInteger.Pow(xP, 2) - 1) * (Foo(2 * yP, p));
                }
                else
                {
                    lyambda = (raznY) * Foo(raznX, p);
                }
                xR = (BigInteger.Pow(lyambda, 2) - xP - xQ);
                yR = yP + lyambda * (xR - xP);
                xR = xR % p < 0 ? (xR % p) + p : xR % p;
                yR = -yR % p < 0 ? (-yR % p) + p : (-yR % p);
            }
            string Result = xR.ToString() + ',' + yR.ToString();
            return Result;
        }
        string Multiply(int k, int xP, int yP)
        {
            string[] numbers = { "", "" };
            int xQ = xP;
            int yQ = yP;
            string[] result = { "" };
            string[] addend = { xQ.ToString(), yQ.ToString() };
            while (k > 0)
            {
                if ((k & 1) > 0)
                {
                    if (result.Length == 2)
                    {
                        result = SumTwoPoints(int.Parse(result[0]), int.Parse(addend[0]), int.Parse(result[1]), int.Parse(addend[1])).Split(',');
                    }
                    else
                    {
                        result = addend;
                    }
                }
                addend = SumTwoPoints(int.Parse(addend[0]), int.Parse(addend[0]), int.Parse(addend[1]), int.Parse(addend[1])).Split(',');
                k >>= 1;
            }
            return result[0] + "," + result[1];
        }
        public void Init()
        {
            hash = new Dictionary<char, (int x, int y)>();
            hash.Add('Д', (194,205));
            hash.Add('И', (198,224));
            hash.Add('А', (189,297));
            hash.Add('Н', (203,427));
            obrhash = new Dictionary<(int x, int y), char>();
            obrhash.Add((194, 205), 'Д');
            obrhash.Add((198, 224), 'И');
            obrhash.Add((189, 297), 'А');
            obrhash.Add((203, 427), 'Н');
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Init();
            string text = TextToEncrypt.Text;
            TextToDecrypt.Text = "";
            (int x, int y) cort;
            int k = 3;
            int xG = 0;
            int yG = 1;
            string[] numbersQ = Multiply(d, xG, yG).Split(',');
            int xQ = int.Parse(numbersQ[0]);
            int yQ = int.Parse(numbersQ[1]);
            foreach (char s in text)
            {
                hash.TryGetValue(s, out cort);
                string numbersC1 = Multiply(k, xG, yG);
                string[] numbersExpr1 = Multiply(k, xQ, yQ).Split(',');
                string numbersC2 = SumTwoPoints(cort.x, int.Parse(numbersExpr1[0]), cort.y, int.Parse(numbersExpr1[1]));
                TextToDecrypt.Text += numbersC1 + ' ' + numbersC2+' ';
            }
            TextToDecrypt.Text = TextToDecrypt.Text.Remove(TextToDecrypt.Text.Length - 1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string[] text = TextToDecrypt.Text.Split(' ');
            TextToEncrypt.Text = "";
            for(int i = 0;i<text.Length;i+=2 )
            {
                string[] numbers1 =text[i].Split(',');
                string[] numbers2 = text[i+1].Split(',');
                string[] numbersC1 = Multiply(d, int.Parse(numbers1[0]), int.Parse(numbers1[1])).Split(',');
                string[] result = SumTwoPoints(int.Parse(numbers2[0]), int.Parse(numbersC1[0]), int.Parse(numbers2[1]), mod(-int.Parse(numbersC1[1]),p)).Split(',');
                (int x, int y) cort = (int.Parse(result[0]), int.Parse(result[1]));
                char s;
                obrhash.TryGetValue(cort, out s);
                TextToEncrypt.Text += s;
            }
        }
        private int Foo(int a, int m)
        {
            int x, y;
            int g = GCD(a, m, out x, out y);
            if (g != 1)
                throw new ArgumentException();
            return (x % m + m) % m;
        }

        private int GCD(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            int x1, y1;
            int d = GCD(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d % p;
        }
    }
}
