using System;
using System.Linq;

namespace Task2_1 {
    class Program {

        const int NumOfStations = 4;
        const int NumOfSuppliers = 6;

        static int[] СostOfDelivery = new int[NumOfSuppliers * NumOfStations] {
                 803, 952, 997, 931,
                 967, 1012, 848, 1200,
                 825, 945, 777, 848,
                 1024, 1800, 931, 999,
                 754, 817, 531, 628,
                 911, 668, 865, 1526 };

        static int[] AmountFromSuppliers = new int[NumOfSuppliers] { 600, 420, 360, 250, 700, 390 };

        static double[] SuppliersPrice = new double[NumOfSuppliers] { 5.2, 4.5, 6.1, 3.8, 6.4, 5.6 };

        static void Main(string[] args) {
            PrintInitialData();

            int SumOfAmountFromSuppliers = AmountFromSuppliers.Sum();

            int[] requiredAmount = new int[NumOfStations];

            int sumOfAmount = 0;
            for (int i = 0; i < NumOfStations; i++) {
                requiredAmount[i] = ReadInt($"необходимое количество топлива на АЗС {(char)('А' + i)}", 0, SumOfAmountFromSuppliers - sumOfAmount);
                sumOfAmount += requiredAmount[i];
            }

            int[,] purchasingTableDefault = CalcPurchasingTableDefault(requiredAmount);
            int[,] purchasingTable = CalcPurchasingTable(requiredAmount);
            int[,] purchasingTable2 = CalcPurchasingTable2(requiredAmount);


            (double cost, double delivery)[] purchaseСostDefault = CalcPurchaseCost(purchasingTableDefault);
            (double cost, double delivery)[] purchaseСost = CalcPurchaseCost(purchasingTable);
            (double cost, double delivery)[] purchaseСost2 = CalcPurchaseCost(purchasingTable2);

            Console.WriteLine();
            Console.WriteLine("Обычный способ:");
            PrintAnswer(purchasingTableDefault, purchaseСostDefault);

            Console.WriteLine();
            Console.WriteLine("Новый способ:");
            PrintAnswer(purchasingTable, purchaseСost);

            Console.WriteLine();
            Console.WriteLine("Новый способ 2:");
            PrintAnswer(purchasingTable2, purchaseСost2);
        }

        static int[,] CalcPurchasingTableDefault(int[] requiredAmountOriginal) {
            int[,] purchasingTable = new int[NumOfSuppliers, NumOfStations];

            int[] amountFromSuppliers = new int[NumOfSuppliers];
            Array.Copy(AmountFromSuppliers, amountFromSuppliers, NumOfSuppliers);

            int[] requiredAmount = new int[NumOfStations];
            Array.Copy(requiredAmountOriginal, requiredAmount, NumOfStations);

            int sum = requiredAmount.Sum();

            while (sum > 0) {
                double minPrice = SuppliersPrice
                    .Select((price, index) => (price, index))
                    .Where(item => amountFromSuppliers[item.index] > 0)
                    .Min(item => item.price);

                int minPriceIndex = SuppliersPrice
                    .Select((price, index) => (price, index))
                    .First(item => amountFromSuppliers[item.index] > 0 && item.price == minPrice).index;

                int minCost = СostOfDelivery
                    .Select((cost, index) => (cost, index))
                    .Where(item => item.index / NumOfStations == minPriceIndex && requiredAmount[item.index % NumOfStations] > 0)
                    .Min(item => item.cost);

                int minCostIndex = СostOfDelivery
                    .Select((cost, index) => (cost, index))
                    .First(item => item.index / NumOfStations == minPriceIndex && requiredAmount[item.index % NumOfStations] > 0 && item.cost == minCost).index % NumOfStations;

                int amount = Math.Min(amountFromSuppliers[minPriceIndex], requiredAmount[minCostIndex]);

                purchasingTable[minPriceIndex, minCostIndex] += amount;
                amountFromSuppliers[minPriceIndex] -= amount;
                requiredAmount[minCostIndex] -= amount;
                sum -= amount;
            }


            return purchasingTable;
        }

        static int[,] CalcPurchasingTable(int[] requiredAmountOriginal) {
            int[,] purchasingTable = new int[NumOfSuppliers, NumOfStations];

            int[] amountFromSuppliers = new int[NumOfSuppliers];
            Array.Copy(AmountFromSuppliers, amountFromSuppliers, NumOfSuppliers);

            int[] requiredAmount = new int[NumOfStations];
            Array.Copy(requiredAmountOriginal, requiredAmount, NumOfStations);

            double[] coef = new double[NumOfStations * NumOfSuppliers];

            int sum = requiredAmount.Sum();
            // максимум среди станций по остатку необходимого топлива, дальше выбор выбор лучшей цены для это заправки.
            while (sum > 0) {
                FillCoef(coef, requiredAmount);
                int maxRequiredAmountIndex = Array.IndexOf(requiredAmount, requiredAmount.Max());


                double minCoef = coef
                    .Select((coef, index) => (coef, index))
                    .Where(item => item.index % NumOfStations == maxRequiredAmountIndex && amountFromSuppliers[item.index / NumOfStations] > 0)
                    .Min(item => item.coef);

                int index = coef
                    .Select((coef, index) => (coef, index))
                    .First(item => item.index % NumOfStations == maxRequiredAmountIndex && amountFromSuppliers[item.index / NumOfStations] > 0 && item.coef == minCoef).index;

                int i = index / NumOfStations;
                int j = index % NumOfStations;

                int amount = Math.Min(amountFromSuppliers[i], requiredAmount[j]);

                purchasingTable[i, j] += amount;
                amountFromSuppliers[i] -= amount;
                requiredAmount[j] -= amount;
                sum -= amount;
            }


            return purchasingTable;
        }

        static int[,] CalcPurchasingTable2(int[] requiredAmountOriginal) {
            int[,] purchasingTable = new int[NumOfSuppliers, NumOfStations];

            int[] amountFromSuppliers = new int[NumOfSuppliers];
            Array.Copy(AmountFromSuppliers, amountFromSuppliers, NumOfSuppliers);

            int[] requiredAmount = new int[NumOfStations];
            Array.Copy(requiredAmountOriginal, requiredAmount, NumOfStations);

            double[] coef = new double[NumOfStations * NumOfSuppliers];

            int sum = requiredAmount.Sum();

            // минимум среди всей таблицы стоимости поставок
            while (sum > 0) {
                FillCoef(coef, requiredAmount);
                double minCoef = coef
                    .Select((coef, index) => (coef, index))
                    .Where(item => requiredAmount[item.index % NumOfStations] > 0 && amountFromSuppliers[item.index / NumOfStations] > 0)
                    .Min(item => item.coef);

                int index = coef
                    .Select((coef, index) => (coef, index))
                    .First(item => requiredAmount[item.index % NumOfStations] > 0 && amountFromSuppliers[item.index / NumOfStations] > 0 && item.coef == minCoef).index;

                int i = index / NumOfStations;
                int j = index % NumOfStations;

                int amount = Math.Min(amountFromSuppliers[i], requiredAmount[j]);

                purchasingTable[i, j] += amount;
                amountFromSuppliers[i] -= amount;
                requiredAmount[j] -= amount;
                sum -= amount;
            }


            return purchasingTable;
        }

        static void FillCoef(double[] coef, int[] requiredAmount) {
            for (int i = 0; i < coef.Length; i++) {
                coef[i] = SuppliersPrice[i / NumOfStations] * requiredAmount[i % NumOfStations] + СostOfDelivery[i]; 
            }
        }

        static (double, double)[] CalcPurchaseCost(int[,] purchasingTable) {
            (double cost, double delivery)[] purchaseCost = new (double, double)[NumOfSuppliers];

            for (int i = 0; i < NumOfSuppliers; i++) {
                for (int j = 0; j < NumOfStations; j++) {
                    if (purchasingTable[i, j] > 0) {
                        purchaseCost[i].cost += purchasingTable[i, j] * SuppliersPrice[i];
                        purchaseCost[i].delivery += СostOfDelivery[i * NumOfStations + j];
                    }
                }
            }

            return purchaseCost;
        }

        static void PrintInitialData() {
            Console.WriteLine("┌────────────┬────────────┬─────────┬───────────────────────────────┐");
            Console.WriteLine("│            │  Максимум  │  Цена   │   Стоимость доставки на АЗС   │");
            Console.WriteLine("│            │   объема   │ закупки ├───────┬───────┬───────┬───────┤");
            Console.WriteLine("│ Поставщики │   закупки  │ за ед.  │   А   │   Б   │   В   │   Г   │");
            Console.WriteLine("├────────────┼────────────┼─────────┼───────┼───────┼───────┼───────┤");

            for (int i = 0; i < NumOfSuppliers; i++) {
                Console.WriteLine($"│{i + 1,12}│{AmountFromSuppliers[i],12}│{SuppliersPrice[i],9}│{СostOfDelivery[i * NumOfStations],7}│{СostOfDelivery[i * NumOfStations + 1],7}│{СostOfDelivery[i * NumOfStations + 2],7}│{СostOfDelivery[i * NumOfStations + 3],7}│");
            }

            Console.WriteLine("└────────────┴────────────┴─────────┴───────┴───────┴───────┴───────┘");
        }

        static void PrintAnswer(int[,] purchasingTable, (double cost, double delivery)[] purchaseСost) {
            Console.WriteLine("┌──────────┬───────┬───────┬───────┬───────┬───────────┬────────────┐");
            Console.WriteLine("│          │       │       │       │       │ Стоимость │  С учетом  │");
            Console.WriteLine("│Поставщики│ АЗС А │ АЗС Б │ АЗС В │ АЗС Г │  закупки  │  доставки  │");
            Console.WriteLine("├──────────┼───────┼───────┼───────┼───────┼───────────┼────────────┤");
            for (int i = 0; i < NumOfSuppliers; i++) {
                Console.WriteLine($"│{i + 1,10}│{purchasingTable[i, 0],7}│{purchasingTable[i, 1],7}│{purchasingTable[i, 2],7}│{purchasingTable[i, 3],7}│{purchaseСost[i].cost,11:F2}│{purchaseСost[i].cost + purchaseСost[i].delivery,12:F2}│");
            }
            Console.WriteLine("└──────────┴───────┴───────┴───────┴───────┴───────────┴────────────┘");
            Console.WriteLine($"Итого: {purchaseСost.Sum(item => item.cost) + purchaseСost.Sum(item => item.delivery):## ###.00}");
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
