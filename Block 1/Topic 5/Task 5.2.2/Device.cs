using System;
using System.Collections.Generic;
using System.Text;

namespace Task_5._2._2 {
    class Device : IInventory {
        public static uint Type { get; } = 3;
        public static uint CountOfDevices { get; private set; } = 0;

        public string Number { get; }
        public Device() {
            Number = InventoryHelper.GetNumber(Type, ++CountOfDevices, DateTime.Now);
        }
    }
}
