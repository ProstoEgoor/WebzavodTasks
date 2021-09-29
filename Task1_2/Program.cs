using System;
using System.Globalization;

namespace Task1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите значение: ");
            string text = Console.ReadLine();

            Console.WriteLine("Тип введенного значения: " + getType(text));
        }

        static string getType(string value)
        {
            NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            if (long.TryParse(value, out _))
            {
                return "целое число";
            } 
            if (double.TryParse(value, style, culture, out _))
            {
                return "дробное число";
            } 
            if (bool.TryParse(value, out _))
            {
                return "логическое значение";
            }
            return "текст";
        }
    }
}
