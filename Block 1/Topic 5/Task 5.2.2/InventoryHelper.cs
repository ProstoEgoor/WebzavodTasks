using System;
using System.Collections.Generic;
using System.Text;

namespace Task_5._2._2 {
    public static class InventoryHelper {
        static uint CountOfObjects { get; set; } = 0;

        public static string GetNumber(uint type, uint countOfCurrentObjects, DateTime dateRegistration) {
            if (type > 99) {
                throw new ArgumentOutOfRangeException("Тип имущества не соответсвует шаблону.");
            }
            if (countOfCurrentObjects > 9999) {
                throw new ArgumentOutOfRangeException("Количество объектов превышает допустимые границы шаблона.");
            }
            if (CountOfObjects > 999999) {
                throw new Exception("Общее количество объектов превышает допустимые границы шаблона.");
            }

            return $"{type:D2}-{countOfCurrentObjects:D4}-{dateRegistration:yy}-{++CountOfObjects:D6}";
        }
    }
}
