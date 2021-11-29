using System;
using System.Linq;
using Task_2._1._1.DAL;
using Task_2._1._1.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Task_2._1._1 {
    class Program {
        static readonly string connectionString = new ConnectionStringManager().ConnectionString;

        static T UsingDBContext<T>(Func<CustomersCardsContext, T> action) {
            var optionBuilder = new DbContextOptionsBuilder<CustomersCardsContext>();
            var options = optionBuilder
                .UseSqlServer(connectionString)
                .Options;

            using var context = new CustomersCardsContext(options);
            return action(context);
        }

        static void Main(string[] args) {
            AddMockData();
            PrintNameAndDiscount();
        }

        static void PrintNameAndDiscount() {
            try {
                var description = UsingDBContext(context => {
                    var description = context
                        .PersonalCards
                        .AsNoTracking()
                        .Select(personalCard => new {
                            personalCard.UserProfile.FirstName,
                            personalCard.UserProfile.LastName,
                            personalCard.Discount
                        }).ToArray();

                    return description;
                });

                foreach (var item in description) {
                    string name = item.FirstName;
                    if (name != null && item.LastName != null) {
                        name += " " + item.LastName;
                    } else if (item.LastName != null) {
                        name = item.LastName;
                    } else {
                        name = "ФИО не задано";
                    }
                    Console.WriteLine($"Пользователь: \"{name}\", имеет скидку: {(int)(item.Discount * 100)}%.");
                }

            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        static void AddMockData() {
            var personalCards = GetMockData();

            try {
                UsingDBContext(context => {
                    context.PersonalCards.AddRange(personalCards);
                    context.SaveChanges();
                    return true;
                });
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        static PersonalCard[] GetMockData() {
            var personalCards = new PersonalCard[3];
            var userProfiles = new UserProfile[3];
            var purchases = new Purchase[7];

            Random random = new Random();

            userProfiles[0] = new UserProfile() {
                Email = "LilaNWatson@armyspy.com",
                FirstName = "Lila",
                LastName = "Watson",
                Birthdate = new DateTime(1991, 2, 5)
            };

            userProfiles[1] = new UserProfile() {
                Email = "Hinatimsee@rhyta.com",
                FirstName = "Глафира",
                LastName = "Евсеева",
                Birthdate = new DateTime(1988, 9, 15)
            };

            userProfiles[2] = new UserProfile() {
                Email = "Apdris1976@jourrapide.com",
            };

            for (int i = 0; i < purchases.Length; i++) {
                purchases[i] = new Purchase {
                    PurchaseSum = (ulong)(random.NextDouble() * 10000)
                };
            }

            var pointers = new int[personalCards.Length + 1];
            pointers[0] = 0;
            pointers[^1] = purchases.Length;

            for (int i = 1; i < pointers.Length - 1; i++) {
                pointers[i] = random.Next(pointers[i - 1], purchases.Length);
            }

            for (int i = 0; i < personalCards.Length; i++) {
                var personalPurchases = new Purchase[pointers[i + 1] - pointers[i]];

                for (int j = 0; j < personalPurchases.Length; j++) {
                    personalPurchases[j] = purchases[pointers[i] + j];
                }

                personalCards[i] = new PersonalCard() {
                    Discount = (float)random.Next(0, 11) / 100,
                    UserProfile = userProfiles[i],
                    Purchases = personalPurchases
                };
            }

            return personalCards;
        }
    }
}
