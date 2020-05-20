﻿using System;
using System.Diagnostics;

namespace DES
{
    public class DES
    {
        public const int sizeOfBlock = 128; //в DES размер блока 64 бит, но поскольку в unicode символ в два раза длинее, то увеличим блок тоже в два раза
        public const int sizeOfChar = 16; //размер одного символа (in Unicode 2 байта)

        public const int shiftKey = 2; //сдвиг ключа 

        public const int quantityOfRounds = 16; //количество раундов

        public static string[] Blocks; //сами блоки в двоичном формате

        public static string decodeKey;
        public static string encodeKey;

        public static void Encrypt(string key)
                //инициализация текста и ключа
                string s = "";
                //строка нарезается на блоки
                CutStringIntoBlocks(s);

        public static void Decrypt(string key)

        //доводим строку до размера, чтобы делилась на sizeOfBlock
        public static string StringToRightLength(string input)

        //разбиение обычной строки на блоки
        public static void CutStringIntoBlocks(string input)

        //довести исходное сообщение до такого размера (в битах), чтобы оно нацело делилось на размер блока (sizeOfBlock = 128 бит);
        public static void CutBinaryStringIntoBlocks(string input)

        //перевод строки в двоичный формат
        public static string StringToBinaryFormat(string input)

        //доводим длину ключа до нужной
        public static string CorrectKeyWord(string input, int lengthKey)

        //шифрование DES один раунд
        public static string EncodeDES_One_Round(string input, string key)

        //расшифровка DES один раунд
        public static string DecodeDES_One_Round(string input, string key)

        //XOR двух строк с двоичными данными
        public static string XOR(string s1, string s2)

        //шифрующая функция f. в данном случае это XOR
        public static string f(string s1, string s2)

        //вычисление ключа для следующего раунда шифрования. циклический сдвиг >> 2
        public static string KeyToNextRound(string key)

        //вычисление ключа для следующего раунда расшифровки. циклический сдвиг << 2
        public static string KeyToPrevRound(string key)

        //переводим строку с двоичными данными в символьный формат
        public static string StringFromBinaryToNormalFormat(string input)
    }
}