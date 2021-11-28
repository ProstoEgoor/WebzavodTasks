using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Task_5._2._1 {
    class Person : IComparable, IComparable<Person> {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public int CompareTo(object obj) {
            if (obj is Person other) {
                return CompareTo(other);
            } else {
                throw new ArgumentException("Object isn't Person.");
            }
        }

        public int CompareTo([AllowNull] Person other) {
            if (other == null) {
                return 1;
            }

            int surnameCompare = Surname.CompareTo(other.Surname);
            if (surnameCompare != 0) {
                return surnameCompare;
            }

            int nameCompare = Name.CompareTo(other.Name);
            if (nameCompare != 0) {
                return nameCompare;
            }

            return BirthDate.CompareTo(other.BirthDate);
        }

        public override string ToString() {
            return $"{Surname} {Name} {BirthDate.ToShortDateString()}";
        }
    }
}
