using System;

namespace Task2_3 {
    class Program {
        static void Main(string[] args) {
            /*int minutes = readInt("количество минут до события");
            Console.WriteLine("До события осталось: " + minutesToString(minutes) + "!");*/

            DateTime eventDate;

            if (args.Length > 0 && DateTime.TryParse(args[0], out eventDate)) {
                Console.WriteLine("Дата события передана в аргументах.");
            } else {
                if (args.Length > 0) {
                    Console.WriteLine("Дата в аргументе задана неправильно.");
                }
                eventDate = readTime("дату и время события");
            }
            TimeSpan interval = eventDate - DateTime.Now;
            Console.WriteLine("До события осталось: " + minutesToString((int) Math.Round(interval.TotalMinutes, 0)) + "!");
        }

        static string minutesToString(int minutes) {
            if (minutes <= 0) {
                return "уже началось";
            }

            int[] time = parseMinutes(minutes);

            string answer = "";
            if (time[0] > 0) {
                answer += String.Format("{0} {1}", time[0], getNumAddition(time[0], "день", "дня", "дней"));
                if (time[1] > 0 || time[2] > 0) {
                    answer += ", ";
                }
            }
            if (time[1] > 0) {
                answer += String.Format("{0} {1}", time[1], getNumAddition(time[1], "час", "часа", "часов"));
                if (time[2] > 0) {
                    answer += ", ";
                }
            }
            if (time[2] > 0) {
                answer += String.Format("{0} {1}", time[2], getNumAddition(time[2], "минута", "минуты", "минут"));
            }

            return answer;
        }

        static string getNumAddition(int num, string first, string second, string third) {
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

        static int[] parseMinutes(int minutes) {
            int[] answer = new int[3];

            answer[0] = minutes / (24 * 60);
            answer[1] = (minutes / 60) % 24;
            answer[2] = minutes % 60;

            return answer;
        }
        static int readInt(string name, int min = int.MinValue, int max = int.MaxValue) {
            Console.Write("Введите " + name + ": ");
            int value;
            while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max) {
                Console.Error.WriteLine("Неверный ввод!");
                Console.Write("Введите " + name + ": ");
            }
            return value;
        }

        static DateTime readTime(string name) {
            Console.Write("Введите " + name + ": ");
            DateTime value;
            while (!DateTime.TryParse(Console.ReadLine(), out value)) {
                Console.Error.WriteLine("Неверный ввод!");
                Console.Write("Введите " + name + ": ");
            }
            return value;
        }
    }
}
