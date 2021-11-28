using System;
using System.Collections.Generic;
using System.Text;

namespace Task_6._1._2 {
    public class AlreadyExistsException : Exception {
        public string Value { get; } 
        public int Position { get; }

        public AlreadyExistsException(string value, int position) {
            Value = value;
            Position = position;
        }
    }
}
