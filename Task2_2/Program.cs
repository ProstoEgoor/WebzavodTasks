using System;

namespace Task2_2 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Введите размеры комнаты:");
            double width = readDouble("ширину", 0, double.MaxValue);
            double length = readDouble("длину", 0, double.MaxValue);
            double height = readDouble("высоту", 0, 20);

            double perimeter = (width + length) * 2;
            double area = perimeter * height;
            double subArea;

            while (true) {
                int numOfDoors = readInt("количество дверей", 0, int.MaxValue);
                int numOfWindows = readInt("количество окон", 0, int.MaxValue);

                subArea = 0;
                double subWidth = 0;

                for (int i = 0; i < numOfDoors; i++) {
                    double w = readDouble(String.Format("ширину {0} двери", i + 1), 0, Math.Max(width, length));
                    subWidth += w;
                    double l = readDouble(String.Format("высоту {0} двери", i + 1), 0, height);

                    subArea += w * l;
                }

                for (int i = 0; i < numOfWindows; i++) {
                    double w = readDouble(String.Format("ширину {0} окна", i + 1), 0, Math.Max(width, length));
                    subWidth += w;
                    double l = readDouble(String.Format("высоту {0} окна", i + 1), 0, height);

                    subArea += w * l;
                }

                if (subWidth > perimeter) {
                    Console.WriteLine("Окна и двери невозможно разместить в помещении. Повторите ввод!");
                } else {
                    break;
                }
            }

            Console.WriteLine("Общая площадь стен с учетом проемов: " + (area - subArea));
        }

        static double readDouble(string name, double min, double max) {
            Console.Write("Введите " + name + ": ");
            double value;
            while (!double.TryParse(Console.ReadLine(), out value) || value < min || value > max) {
                Console.Error.WriteLine("Неверный ввод!");
                Console.Write("Введите " + name + ": ");
            }
            return value;
        }

        static int readInt(string name, int min, int max) {
            Console.Write("Введите " + name + ": ");
            int value;
            while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max) {
                Console.Error.WriteLine("Неверный ввод!");
                Console.Write("Введите " + name + ": ");
            }
            return value;
        }
    }
}
