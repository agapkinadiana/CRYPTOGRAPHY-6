﻿using System;
using System.Diagnostics;

namespace DES
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Ключевое слово: ");
            string keyWord = Console.ReadLine();

            DES.Encrypt(keyWord);
            DES.Decrypt(DES.decodeKey);
        }
    }
}