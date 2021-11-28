using System;
using System.Text.RegularExpressions;

namespace Task_6._2._1 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Введите объем цистерны:");
            Tank tank = new Tank(ReadVolume());
            Console.WriteLine("Для добавления жидкости введите строку в формате \"+ жидкость_в_литрах\"");
            Console.WriteLine("Для убавления жидкости введите строку в формате \"- жидкость_в_литрах\"");
            Regex actionRegex = new Regex(@"^([+-])\s?([1-9]\d*)$");
            while (true) {
                try {
                    string actionLine = Console.ReadLine();
                    Match actionMatch = actionRegex.Match(actionLine);
                    if (actionMatch.Success) {
                        if (actionMatch.Groups[1].Value == "+") {
                            tank.Add(int.Parse(actionMatch.Groups[2].Value));
                        } else {
                            tank.Take(int.Parse(actionMatch.Groups[2].Value));
                        }
                        Console.WriteLine(tank.ToString());
                    } else {
                        Console.WriteLine("Неверный ввод. Повторите ввод.");
                    }
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static int ReadVolume() {
            int answer;
            while (!int.TryParse(Console.ReadLine(), out answer) || answer <= 0) {
                Console.WriteLine("Неверный ввод. Повторите ввод.");
            }

            return answer;
        }
    }
}
