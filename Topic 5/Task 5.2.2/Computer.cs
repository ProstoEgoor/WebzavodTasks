using System;
using System.Collections.Generic;
using System.Text;

namespace Task_5._2._2 {
    class Computer : Device {
        public string CPU { get; set; }
        public ulong HardDiskSize { get; set; }
        public ulong RAMSize { get; set; }

        public Computer() : base() { }
    }
}
