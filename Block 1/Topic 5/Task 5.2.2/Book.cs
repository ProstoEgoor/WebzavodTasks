using System;
using System.Collections.Generic;
using System.Text;

namespace Task_5._2._2 {
    class Book : IInventory {
        public static uint Type { get; } = 13;
        public static uint CountOfBooks { get; private set; } = 0;

        public string Number { get; }
        public string Name { get; set; }
        public string Authors { get; set; }
        public uint YearOfPublishing { get; set; }

        public Book() {
            Number = InventoryHelper.GetNumber(Type, ++CountOfBooks, DateTime.Now);
        }
    }
}
