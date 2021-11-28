using System;
using System.Collections.Generic;

namespace Task_4._2._1 {
    class Program {
        static void Main(string[] args) {
            List<Provider> providers = GenerateProviders();
            Console.WriteLine("Введите часть названия или артикула для поиска:");
            string findLine = Console.ReadLine();
            Console.WriteLine();
            foreach (var provider in providers) {
                IReadOnlyList<Product> findResult = provider.GetFilteredNomenclature(findLine);
                if (findResult.Count > 0) {
                    Console.WriteLine(provider);
                    Console.WriteLine();
                    foreach (var product in findResult) {
                        Console.WriteLine($" {product}");
                    }
                    Console.WriteLine();
                }
            }
        }

        static List<Provider> GenerateProviders() {
            List<Provider> providers = new List<Provider>();
            Random random = new Random(42);

            Manufacturer manufacturer1 = new Manufacturer() {
                TIN = "0358328720",
                Address = "476635, Сахалинская область, город Егорьевск, пр. Сталина, 49",
                Name = "ЕдаТим"
            };
            manufacturer1.AddProduct(new Product[] {
                new Product() { VendorCode = "789976566264490", Name = "Каша свежая",               Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "150439170597738", Name = "Краюшка хлеба хрустящая",   Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "703857311563761", Name = "Курица хрустящая",          Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "917281379382533", Name = "Картошка свежая",           Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "88525028578952",  Name = "Сладкий чай",               Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "993366365471000", Name = "Куриное крыло",             Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "24886882388096",  Name = "Лимонад",                   Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "322988305014356", Name = "Йогурт",                    Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "219793107444489", Name = "Ягненок под мятным соусом", Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "517427381002852", Name = "Сырная пицца",              Price = (decimal) (random.NextDouble() * 1000) },
            });
            providers.Add(manufacturer1);

            Manufacturer manufacturer2 = new Manufacturer() {
                TIN = "2388111704",
                Address = "252120, Нижегородская область, город Чехов, шоссе Чехова, 12",
                Name = "ЛабМода"
            };
            manufacturer2.AddProduct(new Product[] {
                new Product() { VendorCode = "811540500392677", Name = "Джемпер с воротником на молнии",              Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "829553915266351", Name = "Джемпер Eco-conception с V-образным вырезом", Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "589850858097466", Name = "Кардиган из тонкого трикотажа",               Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "734896416038066", Name = "Куртка в байкерском стиле",                   Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "570807463927844", Name = "Брюки стретч",                                Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "359832736318072", Name = "Шерстяное пальто",                            Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "980655542236899", Name = "Джемпер на пуговицах",                        Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "770550517104525", Name = "Легкий дутый жилет",                          Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "128626023515262", Name = "Узкий костюмный жилет",                       Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "416263333874398", Name = "Легкий пуховик",                              Price = (decimal) (random.NextDouble() * 10000) },
            });
            providers.Add(manufacturer2);

            Manufacturer manufacturer3 = new Manufacturer() {
                TIN = "4923503787",
                Address = "878851, Кировская область, город Красногорск, спуск Косиора, 86",
                Name = "МебельАльфа"
            };
            manufacturer3.AddProduct(new Product[] {
                new Product() { VendorCode = "648994431331183", Name = "Стол Комфорт",                   Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "469387341461512", Name = "Стол круглый EAMES D=80",        Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "803074376011032", Name = "Стол обеденный Лофт 120/160*80", Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "499946431329466", Name = "Стол AS60",                      Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "989451488195477", Name = "Стул EAMES",                     Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "165978436328833", Name = "Стул NICOLE",                    Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "654731665743699", Name = "Стул COMFORT",                   Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "399917161224475", Name = "Челси угловой диван",            Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "361377753652996", Name = "Кресло Вояж",                    Price = (decimal) (random.NextDouble() * 10000) },
                new Product() { VendorCode = "778163599395812", Name = "Кресло Вэлс, велюр",             Price = (decimal) (random.NextDouble() * 10000) },
            });
            providers.Add(manufacturer3);

            Manufacturer manufacturer4 = new Manufacturer() {
                TIN = "5308027519",
                Address = "766089, Владимирская область, город Щёлково, пер. Ладыгина, 42",
                Name = "ТехникаПро"
            };
            manufacturer4.AddProduct(new Product[] {
                new Product() { VendorCode = "165351451184390", Name = "Ноутбук Gaming FX506LH-HN004",       Price = (decimal) (random.NextDouble() * 100000) },
                new Product() { VendorCode = "240191344017297", Name = "Ноутбук MacBook Air",                Price = (decimal) (random.NextDouble() * 100000) },
                new Product() { VendorCode = "231929401445475", Name = "Ноутбук Legion 5 15ACH6H",           Price = (decimal) (random.NextDouble() * 100000) },
                new Product() { VendorCode = "492140907975831", Name = "Ноутбук ROG Strix G17 G713QM-HX181", Price = (decimal) (random.NextDouble() * 100000) },
                new Product() { VendorCode = "879924694733728", Name = "Ноутбук MateBook D 16 HVY-WAP9",     Price = (decimal) (random.NextDouble() * 100000) },
                new Product() { VendorCode = "633901819503901", Name = "Ультрабук MateBook D 14 NbB-WAH9",   Price = (decimal) (random.NextDouble() * 100000) },
                new Product() { VendorCode = "204705940561550", Name = "Ноутбук MacBook Pro",                Price = (decimal) (random.NextDouble() * 100000) },
                new Product() { VendorCode = "876790701070",    Name = "Ноутбук GF65 10UE-246XRU",           Price = (decimal) (random.NextDouble() * 100000) },
                new Product() { VendorCode = "510517845129884", Name = "Ноутбук Laptop 15s-fq2000ur",        Price = (decimal) (random.NextDouble() * 100000) },
                new Product() { VendorCode = "588517076053309", Name = "Ноутбук GP66 11UH-054XRU",           Price = (decimal) (random.NextDouble() * 100000) },
            });
            providers.Add(manufacturer4);

            Manufacturer manufacturer5 = new Manufacturer() {
                TIN = "2999094805",
                Address = "858300, Рязанская область, город Солнечногорск, пл. Славы, 88",
                Name = "ВекторСтрой"
            };
            manufacturer5.AddProduct(new Product[] {
                new Product() { VendorCode = "109991239258607", Name = "Обои бумажные Rasch Bambino зелёные",             Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "540098474318000", Name = "Обои бумажные Rasch Kids & Teens мультиколор",    Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "241619895214598", Name = "Обои флизелиновые Barbara Home Collection белые", Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "430057098469484", Name = "Обои флизелиновые Rasch Florentine синие",        Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "311966635005541", Name = "Обои флизелиновые Rasch Florentine серые",        Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "371420664910635", Name = "Обои флизелиновые A.S. Creation Metropolitan",    Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "704648933732659", Name = "Обои флизелиновые Rasch Францгеом",               Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "723387666057638", Name = "Обои флизелиновые Rasch Denzo зелёные",           Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "205292251125799", Name = "Обои флизелиновые Rasch Denzo бежевые",           Price = (decimal) (random.NextDouble() * 1000) },
                new Product() { VendorCode = "636637428710",    Name = "Обои флизелиновые Rasch Florentine серые",        Price = (decimal) (random.NextDouble() * 1000) },
            });
            providers.Add(manufacturer5);

            Dealer dealer1 = new Dealer() {
                TIN = "145822263909",
                Address = "746757, Белгородская область, город Шаховская, пл. Косиора, 24",
                Name = "ШопЕда",
                Manufacturer = manufacturer1,
                ExtraCharge = 0.1m
            };
            providers.Add(dealer1);

            Dealer dealer2 = new Dealer() {
                TIN = "5914721149",
                Address = "268673, Ивановская область, город Домодедово, бульвар Ломоносова, 90",
                Name = "ТетаОдежда",
                Manufacturer = manufacturer2,
                ExtraCharge = 0.25m
            };
            providers.Add(dealer2);

            Dealer dealer3 = new Dealer() {
                TIN = "1544795153",
                Address = "113641, Ивановская область, город Мытищи, ул. Ленина, 99",
                Name = "МебальСолюшнс",
                Manufacturer = manufacturer3,
                ExtraCharge = 0.05m
            };
            providers.Add(dealer3);

            return providers;
        }
    }
}
