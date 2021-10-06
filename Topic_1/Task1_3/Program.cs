using System;

namespace Task1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int number;

            int length = 0;
            int min = int.MaxValue;
            int max = int.MinValue;
            long sum = 0;

            Console.WriteLine("Введите числа:");

            while(TryReadInt(out number))
            {
                ++length;
                if (number < min)
                    min = number;
                if (number > max)
                    max = number;
                sum += number;
            }

            double mean = sum / length;

            Console.WriteLine("Количество чисел:\t" + length);
            Console.WriteLine("Минимальное число:\t" + min);
            Console.WriteLine("Максимальное число:\t" + max);
            Console.WriteLine("Сумма чисел:\t\t" + sum);
            Console.WriteLine("Среднее чисел:\t\t" + mean);
        }

        static bool TryReadInt(out int number)
        {
            string line;
            while(true)
            {
                line = Console.ReadLine();
                if(line.Length == 0)
                {
                    number = 0;
                    return false;
                } else if(int.TryParse(line, out number) && -1000 <= number && number <= 1000)
                {
                    return true;
                } else
                {
                    Console.Error.WriteLine("Неверный ввод! Если хотите закончить ввод введите пустую строку.");
                }
            }
            
        }
    }
}
