using System;
using System.Collections.Generic;
using System.Text;

namespace Task_5._1._1 {
    class B : ISomeInterface {
        int x;
        public int X { get => x; set => x = value; }


        public string Foo(string arg1) { return default; }
        public void Bar(int number) { }

        private void DoSomething(double a) { }
    }
}
