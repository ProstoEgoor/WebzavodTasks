using System;
using System.Text.RegularExpressions​;

namespace Task_4._2._1 {
    public class Product {
        private string vendorCode;
        public string VendorCode {
            get { return vendorCode; }
            set {
                Regex regex = new Regex(@"^\d{10,15}$");
                if (regex.IsMatch(value)) {
                    vendorCode = value;
                } else {
                    throw new ArgumentOutOfRangeException("Некорректный артикул.");
                }

            }
        }

        public string Name { get; set; }

        private decimal price;
        public decimal Price {
            get { return price; }
            set {
                if (value < 0) {
                    throw new ArgumentOutOfRangeException("Цена отрицательная.");
                }
                price = decimal.Round(value, 2);
            }
        }

        public Product() { }
        public Product(Product product, decimal extraCharge) {
            VendorCode = product.VendorCode;
            Name = product.Name;
            Price = product.Price * (1 + extraCharge);
        }

        public override string ToString() {
            return $"\"{Name}\", Артикул: {VendorCode}, Цена: {Price} руб";
        }
    }
}