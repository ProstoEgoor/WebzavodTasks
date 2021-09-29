using System;
using System.Globalization;

namespace Task2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите стоимость: ");

            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint;
            CultureInfo culture = CultureInfo.CurrentCulture;

            decimal cost;
            while (!decimal.TryParse(Console.ReadLine(), style, culture, out cost) || cost < 0 || Math.Round(cost, 2) != cost)
            {
                Console.Error.WriteLine("Неверный ввод! Стоимость должна быть положительным числом, до двух знаков после запятой.");
                Console.Write("Введите стоимость: ");
            }

            decimal VAT, VATForTax;
            decimal costWithoutVAT = getCostWithoutVAT(cost, out VAT, out VATForTax);

            Console.WriteLine("Стоимость без ндс: \t\t" + costWithoutVAT);
            Console.WriteLine("НДС:\t\t\t\t" + VAT);
            Console.WriteLine("НДС для налоговой декларации:\t" + VATForTax);
        }

        static decimal getCostWithoutVAT(decimal cost, out decimal VAT, out decimal VATForTax)
        {
            VAT = Math.Round(cost / 6m, 2);
            VATForTax = Math.Round(VAT, 0);
            return Math.Round(cost / 1.2m, 2);
        }
    }
}
