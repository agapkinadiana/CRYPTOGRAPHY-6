using System;

namespace ELECTRONIC_SIGNATURE
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            RSA.CheckCorrectly();
            RSA.CheckInCorrectly();

            ELGAMAL.CheckCorrectly();
            ELGAMAL.CheckInCorrectly();
        }
    }
}
