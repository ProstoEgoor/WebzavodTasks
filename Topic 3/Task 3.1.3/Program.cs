using System;
using System.Collections.Generic;

namespace Task_3._1._3 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Введите скобочную последовательность для проверки:");
            string line = Console.ReadLine();
            bool[] errorPositions = new bool[line.Length];
            if (CheckBracketSequence(line, errorPositions)) {
                Console.WriteLine("Последовательность правильная.");
            } else {
                Console.WriteLine("Последовательность не правильная.");
                for (int i = 0; i < line.Length; i++) {
                    if (errorPositions[i]) {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    Console.Write(line[i]);
                    Console.ResetColor();
                }
            }
        }

        static bool CheckBracketSequence(string line, bool[] errorPositions) {
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
                    if (bracketStack.Count == 0 || !(bracketPairs[bracketStack.Pop()] == line[i])) {
                        errorPositions[i] = true;
                        return false;
                    }
                }
            }

            if (bracketStack.Count > 0) {
                int pos = line.Length;
                while(bracketStack.Count > 0) {
                    pos = line.LastIndexOf(bracketStack.Pop(), pos - 1);
                    errorPositions[pos] = true;
                }
                return false;
            }

            return true;
        }
    }
}
