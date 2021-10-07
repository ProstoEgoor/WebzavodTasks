using System;
using System.Linq;

namespace Task1_3 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Введите слова:");
            string[] words = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] sortedWords = words.Select(word => word.ToUpper()).OrderBy(word => word).ToArray();
            foreach (var word in sortedWords) {
                Console.WriteLine(word);
            }
        }
    }
}
