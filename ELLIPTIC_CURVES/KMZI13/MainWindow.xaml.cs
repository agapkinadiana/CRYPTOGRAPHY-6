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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;
namespace KMZI13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int mod(int k, int n) { return ((k %= n) < 0) ? k + n : k; }
        int p = 751;
        public MainWindow()
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
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            string[] numbers = PSum.Text.Split(',');
            string[] numbers2 = QSum.Text.Split(',');
            
            int xP = int.Parse(numbers[0]);
            int xQ = int.Parse(numbers2[0]);
            int yP = int.Parse(numbers[1]);
            int yQ = int.Parse(numbers2[1]);
            ResultSum.Text = SumTwoPoints(xP, xQ, yP, yQ);
        }

        private void Multipy_Click(object sender, RoutedEventArgs e)
        {
            int k = int.Parse(kMultiply.Text);
            string[] numbers = PMultiply.Text.Split(',');
            int xP = int.Parse(numbers[0]);
            int yP = int.Parse(numbers[1]);
            ResultMultiply.Text = '(' +Multiply(k, xP, yP)+')';

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int y2;
            double y;
            string num = "";
            for (int i=201;i<236;i++)
            {
                y2 = ((int)Math.Pow(i, 3) - i + 1) % p;
                y = Math.Round(Math.Sqrt(y2),2);
                string ystr = y.ToString().Replace(',','.');
                num += "(" + i + "," + ystr + "),";
            }
            Points.Text = num.Remove(num.Length-1);
        }

        private void Expr_Click(object sender, RoutedEventArgs e)
        {
            int k = int.Parse(kExpr.Text);
            string[] numbers = PExpr.Text.Split(',');
            int xP = int.Parse(numbers[0]);
            int yP = int.Parse(numbers[1]);
            string[] Expr1 =Multiply(k, xP, yP).Split(',');
            int l = int.Parse(lExpr.Text);
            string[] numbers2 = QExpr.Text.Split(',');
            int xQ = int.Parse(numbers2[0]);
            int yQ = int.Parse(numbers2[1]);
            string[] Expr2 = Multiply(l, xQ, yQ).Split(',');
            string[] Expr3 = SumTwoPoints(int.Parse(Expr1[0]), int.Parse(Expr2[0]), int.Parse(Expr1[1]), int.Parse(Expr2[1])).Split(',');
            string[] numbers3 = RExpr.Text.Split(',');
            int xR = int.Parse(numbers3[0]);
            int yR = int.Parse(numbers3[1]);
            ResultExpr.Text='('+SumTwoPoints(int.Parse(Expr3[0]), xR, int.Parse(Expr3[1]), mod(-yR,p))+')';
        }

        private void Expr2_Click(object sender, RoutedEventArgs e)
        {
            string[] numbers = PExpr2.Text.Split(',');
            int xP = int.Parse(numbers[0]);
            int yP = int.Parse(numbers[1]);
            string[] numbers2 = QExpr2.Text.Split(',');
            int xQ = int.Parse(numbers2[0]);
            int yQ = int.Parse(numbers2[1]);
            string[] Expr = SumTwoPoints(xP, xQ, yP, mod(-yQ,p)).Split(',');
            string[] numbers3 = RExpr2.Text.Split(',');
            int xR = int.Parse(numbers3[0]);
            int yR = int.Parse(numbers3[1]);
            ResultExpr2.Text= SumTwoPoints(int.Parse(Expr[0]), xR, int.Parse(Expr[1]), yR);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ECP win = new ECP();
            win.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Encrypt win = new Encrypt();
            win.Show();
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

        private void PSum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
