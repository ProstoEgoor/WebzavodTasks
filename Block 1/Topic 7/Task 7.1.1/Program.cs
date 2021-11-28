using System;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.Text.Unicode;

namespace Task_7._1._1 {
    class Program {
        static void Main(string[] args) {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            List<Address> addresses = MockAddresses();
            string jsonAddresses = JsonSerializer.Serialize(addresses, options);
            Console.WriteLine(jsonAddresses);
        }

        private static List<Address> MockAddresses() {
            List<Address> addresses = new List<Address>();
            addresses.Add(new Address() {
                PostCode = "084609",
                Country = "Россия",
                City = "Подольс",
                Street = "шоссе Гагарина",
                Building = "81"
            });
            addresses.Add(new Address() {
                PostCode = "921457",
                Country = "Россия",
                City = "Ступино",
                Street = "въезд Чехова",
                Building = "05"
            });
            addresses.Add(new Address() {
                PostCode = "790884",
                Country = "Россия",
                City = "Павловский Посад",
                Street = "спуск Гагарина",
                Building = "40"
            });
            addresses.Add(new Address() {
                PostCode = "YO11 3JN",
                Country = "United Kingdom",
                City = "Barry",
                Street = "Patel Spur East",
                Building = "7"
            });
            addresses.Add(new Address() {
                PostCode = "IG1 3TR",
                Country = "United Kingdom",
                City = "Khanberg",
                Street = "Eva Valley",
                Building = "Studio 59p"
            });

            return addresses;
        }
    }
}
