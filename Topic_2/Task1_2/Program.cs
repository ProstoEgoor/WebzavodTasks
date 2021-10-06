using System;
using System.Collections.Generic;
using System.Linq;

namespace Task1_2 {
    class Program {
        static void Main(string[] args) {
            int length = ReadInt("длину желаемого массива", 1, int.MaxValue);
            int[] values = GenerateArray(length);
            Array.Sort(values);
            int[] mode = GetMode(values);
            double median = getMedian(values);

            Console.WriteLine();
            Console.WriteLine($"Сгенерированный массив: {String.Join(", ", values)};");
            Console.WriteLine();
            Console.WriteLine($"Мода: {String.Join(", ", mode)}. Медиана: {median}.");
        }

        static int[] GenerateArray(int length, int seed = 0, int min = 0, int max = 101) {
            Random random = seed == 0 ? new Random() : new Random(seed);
            int[] array = new int[length];
            for (int i = 0; i < length; i++) {
                array[i] = random.Next(min, max);
            }
            return array;
        }

        static double getMedian(int[] values) {
            if (values.Length < 1) {
                throw new Exception("Array is empty.");
            }

            if (values.Length == 1) {
                return values[0];
            }

            if (values.Length % 2 == 0) {
                
                return (values[values.Length / 2 - 1] + values[values.Length / 2]) / 2.0;
            } else {
                return values[values.Length / 2];
            }
        }

        static int[] GetMode(int[] values) {
            if (values.Length < 1) {
                throw new Exception("Array is empty.");
            }
            int currentValue = values[0];
            int numberOfCurrentValues = 0;
            int maxNumberOfCurrentValues = 0;
            int numberOfModes = 0;


            foreach (int value in values) {
                if (value == currentValue) {
                    ++numberOfCurrentValues;
                } else {
                    currentValue = value;
                    numberOfCurrentValues = 1;
                }

                if (maxNumberOfCurrentValues < numberOfCurrentValues) {
                    maxNumberOfCurrentValues = numberOfCurrentValues;
                    numberOfModes = 1;
                } else if (maxNumberOfCurrentValues == numberOfCurrentValues) {
                    ++numberOfModes;
                }
            }

            int[] answer = new int[numberOfModes];
            int index = 0;
            currentValue = values[0];
            numberOfCurrentValues = 0;

            foreach (int value in values) {
                if (value == currentValue) {
                    ++numberOfCurrentValues;
                } else {
                    currentValue = value;
                    numberOfCurrentValues = 1;
                }

                if (maxNumberOfCurrentValues == numberOfCurrentValues) {
                    answer[index++] = value;
                }
            }

            return answer;
        }

        static int ReadInt(string name, int min, int max) {
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
