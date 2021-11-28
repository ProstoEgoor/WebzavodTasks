using System;
using System.Collections.Generic;

namespace Task_6._1._2 {
    class Program {
        static void Main(string[] args) {
            Dictionary<string, int> enteredValues = new Dictionary<string, int>();
            int position = 1;
            try {
                while (true) {
                    Console.Write($"Введите значение №{position}: ");
                    string value = Console.ReadLine().Trim().ToLower();
                    if (enteredValues.ContainsKey(value)) {
                        throw new AlreadyExistsException(value, enteredValues[value]);
                    } else {
                        enteredValues[value] = position++;
                    }
                }
            } catch (AlreadyExistsException e) {
                Console.WriteLine($"Значение \"{e.Value}\" уже было введенно раннее на позиции №{e.Position}.");
            }
        }
    }
}
