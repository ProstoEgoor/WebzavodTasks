using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Task_7._1._1 {
    class Address {
        [JsonIgnore]
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
    }
}
