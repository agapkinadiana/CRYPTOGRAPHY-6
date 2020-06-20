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
    /// Логика взаимодействия для ECP.xaml
    /// </summary>
    public partial class ECP : Window
    {
        int mod(int k, int n) { return ((k %= n) < 0) ? k + n : k; }
        Dictionary<char, int> hash;
        int p = 751;
        public ECP()
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
            hash = new Dictionary<char, int>();
            hash.Add('Д', 194);
            hash.Add('И', 198);
            hash.Add('А', 189);
            hash.Add('Н', 203);
        }

        private void WithString(string str)
        {
            int hashstr = 0;
            int outval;
            int q = 13;
            foreach (char s in str)
            {
                hash.TryGetValue(s, out outval);
                hashstr += outval % q;
            }
            int d = 10;
            string[] numbersQ = Multiply(d, 416, 55).Split(',');
            string[] numbersG = Multiply(6, 416, 55).Split(',');
            int x = int.Parse(numbersG[0]);
            int r = mod(x, q) + 4;
            int t = 11 % q;
            int sign = mod((t * (hashstr + d * r)), q);
            Sign.Text = r.ToString() + ',' + sign.ToString();
            int w = mod(Foo(sign, q), q);
            int u1 = mod((w * hashstr), q);
            int u2 = mod((w * r), q);
            string[] Expr1 = { "", "" };
            string[] Expr2 = { "", "" };
            Expr1 = Multiply(u1, int.Parse(numbersG[0]), int.Parse(numbersG[1])).Split(',');
            Expr2 = Multiply(u2, int.Parse(numbersQ[0]), int.Parse(numbersQ[1])).Split(',');
            string[] result;
            result = SumTwoPoints(int.Parse(Expr1[0]), int.Parse(Expr2[0]), int.Parse(Expr1[1]), int.Parse(Expr2[1])).Split(',');
            int v = mod(int.Parse(result[0]), q);
            bool f = false;
            if (v != r)
            {
                f = true;
                Verify.Text = f.ToString();
            }
            else
            {
                Verify.Text = f.ToString();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Init();
            WithString("ДИАНА");
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
            return d%p;
        }
    }
}
