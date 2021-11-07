using System;
using System.Collections.Generic;

namespace Task_5._2._1 {
    class Program {
        static void Main(string[] args) {
            List<Person> people = GetMockPeople();
            List<Person> sortPeople = new List<Person>(people);
            sortPeople.Sort();
            List<Person> reverseSortPeople = new List<Person>(sortPeople);
            reverseSortPeople.Reverse();

            Console.WriteLine("Сортированный в прямом порядке список людей:");
            foreach (var person in sortPeople) {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Сортированный в обратном порядке список людей:");
            foreach (var person in reverseSortPeople) {
                Console.WriteLine(person.ToString());
            }
        }

        private static List<Person> GetMockPeople() {
            List<Person> people = new List<Person> {
                new Person() { Name = "Мокей", Surname = "Фролов" , BirthDate = DateTime.Parse("28 ноября 2001")},
                new Person() { Name = "Любомира", Surname = "Тюрина", BirthDate = DateTime.Parse("17 мая 1991")},
                new Person() { Name = "Власта", Surname = "Тюрина", BirthDate = DateTime.Parse("23 октября 1990")},
                new Person() { Name = "Амос", Surname = "Новицкий", BirthDate = DateTime.Parse("12 октября 1984")},
                new Person() { Name = "Исидор", Surname = "Тихомиров", BirthDate = DateTime.Parse("8 ноября 2001")},
                new Person() { Name = "Викторин", Surname = "Ефимов", BirthDate = DateTime.Parse("31 августа 2000")},
                new Person() { Name = "Пров", Surname = "Попов", BirthDate = DateTime.Parse("25 февраля 1992")},
                new Person() { Name = "Афанасий", Surname = "Лапин", BirthDate = DateTime.Parse("1 июля 1980")},
            };
            return people;
        }
    }
}
