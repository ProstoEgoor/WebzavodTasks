using System;
using System.Collections.Generic;

namespace Task_3._1._3 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Введите скобочную последовательность для проверки:");
            string line = Console.ReadLine();
            Console.WriteLine(CheckBracketSequence(line));
        }

        static bool CheckBracketSequence(string line) {
            Dictionary<char, char> bracketPairs = new Dictionary<char, char> {
                ['('] = ')',
                ['{'] = '}',
                ['['] = ']',
                ['<'] = '>'
            };

            Stack<char> bracketStack = new Stack<char>();

            for (int i = 0; i < line.Length; i++) {
                if (bracketPairs.ContainsKey(line[i])) {
                    bracketStack.Push(line[i]);
                } else if (bracketPairs.ContainsValue(line[i])) {
                    if  (bracketStack.Count == 0 || !(bracketPairs[bracketStack.Pop()] == line[i])) {
                        return false;
                    }
                }
            }

            if (bracketStack.Count > 0) {
                return false;
            }

            return true;
        }
    }
}
