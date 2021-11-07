using System;
using System.Collections.Generic;
using System.Text;

namespace Task_5._1._1 {
    class A : ISomeInterface {
        int x;
        public int X { get => x; set => x = value; }

        public double Y { get; set; }

        public string Foo(string arg1) { return default; }

        public A Process(A a) { return default; }
        public void Bar(int number) { }
    }
}
