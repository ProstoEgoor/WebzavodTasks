using System;
using System.Threading;

namespace Task_7._2._2 {
    class Program {
        static void Main(string[] args) {
            ConsoleItem consoleItem = new DirectoryView(null, null);
            while (consoleItem != null) {
                consoleItem.Show();
                consoleItem.HandleKeyStroke(Console.ReadKey(true), out _);
                consoleItem = consoleItem.Next;
            }
            Console.Clear();
        }
    }
}
