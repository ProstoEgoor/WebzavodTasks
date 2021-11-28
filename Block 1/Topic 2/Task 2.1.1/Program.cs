using System;

namespace Task1_1 {
    class Program {
        static void Main(string[] args) {
            ConsoleKeyInfo pressedKey;
            while (true) {
                pressedKey = Console.ReadKey(true);
                if(pressedKey.KeyChar == 0 
                    || pressedKey.Key == ConsoleKey.Spacebar
                    || pressedKey.Key == ConsoleKey.Enter
                    || pressedKey.Key == ConsoleKey.Backspace
                    || pressedKey.Key == ConsoleKey.Escape
                    || pressedKey.Key == ConsoleKey.Tab) {
                    Console.WriteLine($"Введенная клавиша: {pressedKey.Key}, код клавиши отсутствует.");
                } else {
                    Console.WriteLine($"Введенная клавиша: {pressedKey.KeyChar}, код клавиши: 0x{(int)pressedKey.KeyChar:x4}");
                }
            }
        }
    }
}
