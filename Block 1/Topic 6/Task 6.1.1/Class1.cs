using System;
using System.Collections.Generic;
using System.Text;

namespace Task_6._1._1 {
    class Class1 {

        public static void Main() { }

        public delegate Class1 GetNewClass1();
        public delegate Class1 TransformClass1(Class1 obj);
        public delegate string GetClass1Description(Class1 obj, string comment);

        GetNewClass1 Generator;
        TransformClass1 Transformer;

        public Class1(GetClass1Description d1, GetClass1Description d2) {
            OnDescribe += d1;
            OnDescribe += d2;
        }

        public event GetClass1Description OnDescribe;
    }
}
