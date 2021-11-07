using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task_4._2._1 {
    public class Dealer : Provider {
        public Manufacturer Manufacturer { get; set; }

        private decimal extraCharge;
        public decimal ExtraCharge {
            get { return extraCharge; }
            set { 
                if (value < 0) {
                    throw new ArgumentOutOfRangeException("Наценка отрицательная.");
                }
                extraCharge = value;
            }
        }

        public override IReadOnlyList<Product> Nomenclature => Manufacturer.Nomenclature.Select(product => new Product(product, ExtraCharge)).ToList();

        public override string ToString() {
            return $"Дилер \"{Name}\" (Производитель \"{Manufacturer.Name}\")\r\nАдрес: {Address}";
        }
    }
}
