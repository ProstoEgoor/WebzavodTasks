using System;
using System.Collections.Generic;

namespace Task_5._2._2 {
    class Program {
        static void Main(string[] args) {
            List<IInventory> objects = new List<IInventory> {
                new Building(),
                new Building(),
                new Building(),
                new Building(),
                new Device(),
                new Device(),
                new Device(),
                new Device(),
                new Computer(),
                new Computer(),
                new Computer(),
                new Computer(),
                new Book(),
                new Book(),
                new Book(),
                new Book(),
            };

            foreach (var inventoriableObject in objects) {
                Console.WriteLine(inventoriableObject.Number);
            }
        }
    }
}
