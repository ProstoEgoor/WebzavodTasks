using System;
using System.Collections.Generic;
using System.Text;

namespace Task_5._2._2 {
    class Building : IInventory {
        public static uint Type { get; } = 1;
        public static uint CountOfBuildings { get; private set; } = 0;
        public string Number { get; }
        public string Address { get; set; }
        public Building() {
            Number = InventoryHelper.GetNumber(Type, ++CountOfBuildings, DateTime.Now);
        }
    }
}
