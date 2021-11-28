using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_3._1._1 {
    class Program {
        static Dictionary<string, string> CountryСapitals { get; } = new Dictionary<string, string>();

        static Program() {
            CountryСapitals["Нидерланды"] = "Амстердам";
            CountryСapitals["Андорра"] = "Андорра-ла-Велья";
            CountryСapitals["Греция"] = "Афины";
            CountryСapitals["Сербия"] = "Белград";
            CountryСapitals["Германия"] = "Берлин";
            CountryСapitals["Швейцария"] = "Берн";
            CountryСapitals["Словакия"] = "Братислава";
            CountryСapitals["Бельгия"] = "Брюссель";
            CountryСapitals["Венгрия"] = "Будапешт";
            CountryСapitals["Румыния"] = "Бухарест";
            CountryСapitals["Лихтенштейн"] = "Вадуц";
            CountryСapitals["Мальта"] = "Валлетта";
            CountryСapitals["Польша"] = "Варшава";
            CountryСapitals["Ватикан"] = "Ватикан";
            CountryСapitals["Австрия"] = "Вена";
            CountryСapitals["Литва"] = "Вильнюс";
            CountryСapitals["Ирландия"] = "Дублин";
            CountryСapitals["Хорватия"] = "Загреб";
            CountryСapitals["Украина"] = "Киев";
            CountryСapitals["Молдавия"] = "Кишинёв";
        }

        static void Main(string[] args) {
            bool exit = false;
            while (!exit) {
                Console.WriteLine("Введите 0, чтобы выйти.");
                Console.WriteLine("Введите 1, чтобы показать все заполненные страны.");
                Console.WriteLine("Введите 2, чтобы показать количество заполненных стран.");
                Console.WriteLine("Введите 3, чтобы найти страну.");
                Console.WriteLine("Введите 4, чтобы добавить страну.");
                Console.WriteLine("Введите 5, чтобы удалить страну.");

                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.WriteLine();

                switch (key) {
                    case '0': exit = true; break;
                    case '1': PrintAllCountries(); break;
                    case '2': PrintCountOfCountries(); break;
                    case '3': FindCountry(); break;
                    case '4': AddCountry(); break;
                    case '5': RemoveCountry(); break;
                }
                Console.WriteLine();
            }
        }

        static void AddCountry() {
            while (true) {
                Console.Write("Введите название страны: ");
                string country = Console.ReadLine().Trim();
                if (CountryСapitals.ContainsKey(country)) {
                    Console.WriteLine($"Страна с названием: {country} уже существует.");
                    if (!YesNoDialog("Перезаписать страну?")) {
                        if (YesNoDialog("Повторить ввод названия страны?")) {
                            continue;
                        } else {
                            break;
                        }
                        
                    } 
                }

                Console.Write("Введите название столицы страны: ");
                string capital = Console.ReadLine().Trim();
                CountryСapitals[country] = capital;
                Console.WriteLine("Страна усппешно добавлена");
                break;
            }
        }

        static void RemoveCountry() {
            if (CheckCountOfCountries()) {
                while (true) {
                    Console.Write("Введите название страны, которую хотите удалить: ");
                    string country = Console.ReadLine().Trim();
                    if (CountryСapitals.Remove(country)) {
                        Console.WriteLine($"Страна с названием: {country} успешно удалена.");
                        break;
                    } else {
                        Console.WriteLine($"Страна с названием: {country} не найдена.");
                        if (!YesNoDialog("Повторить ввод страны?")) {
                            break;
                        }
                    }
                }
            }
        }

        static void FindCountry() {
            if (CheckCountOfCountries()) {
                while (true) {
                    Console.Write("Введите название или столицу страны: ");
                    string line = Console.ReadLine().Trim();

                    if (CountryСapitals.ContainsKey(line)) {
                        Console.WriteLine($"Найдена страна с названием: {line}, ее столица: {CountryСapitals[line]}");
                        break;
                    } else {
                        string country = CountryСapitals.FirstOrDefault(item => item.Value == line).Key;
                        if (country != null) {
                            Console.WriteLine($"Найдена страна со столицей: {line}, ее название: {country}");
                            break;
                        } else {
                            Console.WriteLine("Страна с таким названием или такой столицей не найдена.");
                            if (!YesNoDialog("Повторить поиск?")) {
                                break;
                            }
                        }
                    }
                }
            }
        }

        static void PrintAllCountries() {
            if (CheckCountOfCountries()) {
                Console.WriteLine(string.Format($"{{0,-{Console.WindowWidth / 2}}}{{1,-{Console.WindowWidth / 2}}}", "Страна", "Столица"));
                Console.WriteLine();
                foreach (var item in CountryСapitals) {
                    Console.WriteLine(string.Format($"{{0,-{Console.WindowWidth / 2}}}{{1,-{Console.WindowWidth / 2}}}", item.Key, item.Value));
                }
            } 
        }

        static void PrintCountOfCountries() {
            Console.WriteLine($"Заполнено {CountryСapitals.Count} {GetNumAddition(CountryСapitals.Count, "страна", "страны", "стран")}.");
        }

        static string GetNumAddition(int num, string first, string second, string third) {
            int preLastDigit = num % 100 / 10;

            if (preLastDigit == 1) {
                return third;
            }

            switch (num % 10) {
                case 1:
                    return first;
                case 2:
                case 3:
                case 4:
                    return second;
                default:
                    return third;
            }

        }

        static bool YesNoDialog(string question) {
            while (true) {
                Console.WriteLine($"{question} Введите да или нет: ");
                string answer = Console.ReadLine().Trim();
                if (answer == "да") {
                    return true;
                }
                if (answer == "нет") {
                    return false;
                }
                Console.WriteLine("Неправильный ввод.");
            }
        }

        static bool CheckCountOfCountries() {
            if (CountryСapitals.Count == 0) {
                Console.WriteLine("Заполненных стран не найдено.");
                return false;
            }

            return true;
        }
    }
}
