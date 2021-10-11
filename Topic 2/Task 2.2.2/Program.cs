using System;
using System.Text;

namespace Task2_2 {
    class Program {

        static char[] LowerLetters = { 'a', 'b', 'v', 'g', 'd', 'e', 'ž', 'z', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 't', 'u', 'f', 'h', 'c', 'č', 'š', 'ŝ', 'ʺ', 'y', 'ʹ', 'è', 'û', 'â', 'ë' };
        static char[] UpperLetters = { 'A', 'B', 'V', 'G', 'D', 'E', 'Ž', 'Z', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'F', 'H', 'C', 'Č', 'Š', 'Ŝ', 'ʺ', 'Y', 'ʹ', 'È', 'Û', 'Â', 'Ë' };

        static void Main(string[] args) {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Введите текст для транслитерации:");
            string text = Translit(Console.ReadLine());
            Console.WriteLine("Текст после транслитерации:");
            Console.WriteLine(text);
        }

        static string Translit(string text) {
            StringBuilder sb = new StringBuilder(text);
            for (int i = 0; i < sb.Length; i++) {
                sb[i] = GetTranslitLetter(sb[i]);
            }

            return sb.ToString();
        }

        static char GetTranslitLetter(char letter) {
            if ('а' <= letter && letter <= 'я') {
                return LowerLetters[letter - 'а'];
            }
            if ('ё' == letter) {
                return LowerLetters[LowerLetters.Length - 1];
            }

            if ('А' <= letter && letter <= 'Я') {
                return UpperLetters[letter - 'А'];
            }
            if ('Ё' == letter) {
                return UpperLetters[LowerLetters.Length - 1];
            }

            return letter;
        }
    }
}
