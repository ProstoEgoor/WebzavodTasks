using System;
using System.Collections.Generic;
using System.Text;

namespace Task_4._1._1 {
    abstract class A {

        public static void Main() { }

        public string Prop1 { get; set; } = "aaa";

        private int prop2;
        public int Prop2 {
            get { return prop2; }
            set {
                prop2 = value > 100 ? 100 : value;
                prop2 = value < 0 ? 0 : prop2;
            }
        }

        public static int Foo() { return default; }

        public abstract void Foo(DateTime date);
    }
}
