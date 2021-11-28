using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions​;
using System.Linq;

namespace Task_4._2._1 {
    public abstract class Provider {
        private string tin;
        public string TIN {
            get { return tin; }
            set {
                Regex TINregex = new Regex(@"^\d{10}$");
                Regex ITNregex = new Regex(@"^\d{12}$");
                if (TINregex.IsMatch(value) || ITNregex.IsMatch(value)) {
                    tin = value;
                } else {
                    throw new ArgumentOutOfRangeException("Некорректный ИНН.");
                }
            }
        }

        public string Name { get; set; }
        public string Address { get; set; }

        public abstract IReadOnlyList<Product> Nomenclature { get; }

        public IReadOnlyList<Product> GetFilteredNomenclature(string argument) {
            return Nomenclature.Where(product => product.Name.Contains(argument, StringComparison.OrdinalIgnoreCase) || product.VendorCode.Contains(argument)).ToList();
        }
    }
}
