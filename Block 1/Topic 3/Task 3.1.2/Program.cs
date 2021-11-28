using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_3._1._2 {
    class Program {

        static List<HashSet<string>> UsersFavoriteObjects { get; set; }
        static Dictionary<string, int> FrequencyOfFavoriteObjects { get; set; }

        static void Main(string[] args) {
            UsersFavoriteObjects = WriteUsersFavoriteObjects();
            Console.WriteLine();
            FrequencyOfFavoriteObjects = IdentifyFrequencyOfFavoriteObjects(UsersFavoriteObjects);

            if (UsersFavoriteObjects.Count > 0) {
                ShowThatEveryoneLoves(UsersFavoriteObjects, FrequencyOfFavoriteObjects);
                ShowAllFavoriteObjects(UsersFavoriteObjects, FrequencyOfFavoriteObjects);
                Console.WriteLine();
                ShowUniqueFavoriteObjects(UsersFavoriteObjects, FrequencyOfFavoriteObjects);
                Console.WriteLine();
                ShowFrequencyOfFavoriteObjects(FrequencyOfFavoriteObjects);
            }
        }

        static List<HashSet<string>> WriteUsersFavoriteObjects() {
            List<HashSet<string>> usersFavoriteObjects = new List<HashSet<string>>();

            Console.WriteLine("Введите любимки пользователей через запятую.");
            Console.WriteLine("Для завершения ввода для одного пользователя введите enter.");
            Console.WriteLine("Для завершения ввода любимок пользоватей введите пустую строку");

            string line;
            while (true) {
                line = Console.ReadLine().Trim().ToLower();

                if (line != "") {
                    string[] favoriteObjects = line.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(favoriteObject => string.Join(' ', favoriteObject.Split(' ', StringSplitOptions.RemoveEmptyEntries)))
                        .ToArray();

                    usersFavoriteObjects.Add(new HashSet<string>(favoriteObjects));
                } else {
                    break;
                }

            }

            return usersFavoriteObjects;
        }

        static Dictionary<string, int> IdentifyFrequencyOfFavoriteObjects(List<HashSet<string>> usersFavoriteObjects) {
            Dictionary<string, int> frequencyOfFavoriteObjects = new Dictionary<string, int>();

            foreach (var userFavoriteObjects in usersFavoriteObjects) {
                foreach (var favoriteObject in userFavoriteObjects) {
                    if (frequencyOfFavoriteObjects.ContainsKey(favoriteObject)) {
                        frequencyOfFavoriteObjects[favoriteObject]++;
                    } else {
                        frequencyOfFavoriteObjects[favoriteObject] = 1;
                    }
                }
            }

            return frequencyOfFavoriteObjects;
        }

        static void ShowThatEveryoneLoves(List<HashSet<string>> usersFavoriteObjects, Dictionary<string, int> favoriteObjects) {
            HashSet<string> answer = new HashSet<string>();

            /*answer.UnionWith(usersFavoriteObjects[0]);
            foreach (var userfavoriteObjects in usersFavoriteObjects) {
                answer.IntersectWith(userfavoriteObjects);
            }*/

            foreach (var favoriteObjectPair in favoriteObjects) {
                if (favoriteObjectPair.Value == usersFavoriteObjects.Count) {
                    answer.Add(favoriteObjectPair.Key);
                }
            }

            if (answer.Count == 0) {
                Console.WriteLine("Общих любимок не найдено.");
            } else {
                if (answer.Count == 1) {
                    Console.Write("Найдена общая любимка: ");
                } else {
                    Console.Write("Найдены общие любимки: ");
                }

                Console.Write(string.Join(", ", answer));
                Console.WriteLine(";");
            }
        }

        static void ShowAllFavoriteObjects(List<HashSet<string>> usersFavoriteObjects, Dictionary<string, int> favoriteObjects) {
            HashSet<string> answer = new HashSet<string>();

            /*foreach (var userFavoriteObjects in usersFavoriteObjects) {
                answer.UnionWith(userFavoriteObjects);
            }*/

            answer.UnionWith(favoriteObjects.Select(item => item.Key));

            Console.Write("Все встречающиеся любимки: ");
            Console.Write(string.Join(", ", answer));
            Console.WriteLine(";");
        }

        static void ShowUniqueFavoriteObjects(List<HashSet<string>> usersFavoriteObjects, Dictionary<string, int> favoriteObjects) {
            List<HashSet<string>> answer = new List<HashSet<string>>();

            /*foreach (var userFavoriteObjects in usersFavoriteObjects) {
                answer.Add(new HashSet<string>(userFavoriteObjects));
            }

            for (int i = 0; i < usersFavoriteObjects.Count; i++) {
                for (int j = 0; j < usersFavoriteObjects.Count; j++) {
                    if (j != i) {
                        answer[i].ExceptWith(usersFavoriteObjects[j]);
                    }
                }
            }*/

            for (int i = 0; i < usersFavoriteObjects.Count; i++) {
                answer.Add(new HashSet<string>());
                foreach (var favoriteObject in usersFavoriteObjects[i]) {
                    if (favoriteObjects[favoriteObject] == 1) {
                        answer[i].Add(favoriteObject);
                    }
                }
            }

            for (int i = 0; i < answer.Count; i++) {
                if (answer[i].Count > 0) {
                    Console.Write($"Уникальные любимки пользователя № {i + 1}: ");
                    Console.Write(string.Join(", ", answer[i]));
                    Console.WriteLine(";");
                } else {
                    Console.WriteLine($"Пользователь № {i + 1} не имеет уникальных любимок.");
                }
            }
        }

        static void ShowFrequencyOfFavoriteObjects(Dictionary<string, int> favoriteObjects) {
            Console.WriteLine(string.Format($"{{0,-{Console.WindowWidth / 2}}}{{1,-{Console.WindowWidth / 2}}}", "Любимка", "частота использования"));
            Console.WriteLine();
            foreach (var favoriteObjectPair in favoriteObjects) {
                Console.WriteLine(string.Format($"{{0,-{Console.WindowWidth / 2}}}{{1,-{Console.WindowWidth / 2}}}", favoriteObjectPair.Key, favoriteObjectPair.Value));
            }
        }
    }
}
