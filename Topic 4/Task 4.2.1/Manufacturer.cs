using System;
using System.Collections.Generic;
using System.Text;

namespace Task_4._2._1 {
    public class Manufacturer : Provider {
        List<Product> NomenclatureList { get; set; } = new List<Product>();

        public void AddProduct(params Product[] product) {
            NomenclatureList.AddRange(product);
        }
        public bool RemoveProduct(Product product) {
            return NomenclatureList.Remove(product);
        }

        public override string ToString() {
            return $"Производитель \"{Name}\"\r\nАдрес: {Address}";
        }

        public override IReadOnlyList<Product> Nomenclature => NomenclatureList;
    }
}
