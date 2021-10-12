using System;
using System.Threading;

namespace Task_2._2._3 {
    class Program {

        static bool listnerOn;
        static bool updateNeed;
        static bool isUpdate;


        static string text = "Что такое Lorem Ipsum?\n\rLorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. Lorem Ipsum не только успешно пережил без заметных изменений пять веков, но и перешагнул в электронный дизайн. Его популяризации в новое время послужили публикация листов Letraset с образцами Lorem Ipsum в 60-х годах и, в более недавнее время, программы электронной вёрстки типа Aldus PageMaker, в шаблонах которых используется Lorem Ipsum.\n\r\n\rПочему он используется?\n\rДавно выяснено, что при оценке дизайна и композиции читаемый текст мешает сосредоточиться. Lorem Ipsum используют потому, что тот обеспечивает более или менее стандартное заполнение шаблона, а также реальное распределение букв и пробелов в абзацах, которое не получается при простой дубликации \"Здесь ваш текст..Здесь ваш текст..Здесь ваш текст..\" Многие программы электронной вёрстки и редакторы HTML используют Lorem Ipsum в качестве текста по умолчанию, так что поиск по ключевым словам \"lorem ipsum\" сразу показывает, как много веб-страниц всё ещё дожидаются своего настоящего рождения. За прошедшие годы текст Lorem Ipsum получил много версий. Некоторые версии появились по ошибке, некоторые - намеренно (например, юмористические варианты).\n\r\n\rОткуда он появился?\n\rМногие думают, что Lorem Ipsum - взятый с потолка псевдо-латинский набор слов, но это не совсем так. Его корни уходят в один фрагмент классической латыни 45 года н.э., то есть более двух тысячелетий назад. Ричард МакКлинток, профессор латыни из колледжа Hampden-Sydney, штат Вирджиния, взял одно из самых странных слов в Lorem Ipsum, \"consectetur\", и занялся его поисками в классической латинской литературе. В результате он нашёл неоспоримый первоисточник Lorem Ipsum в разделах 1.10.32 и 1.10.33 книги \"de Finibus Bonorum et Malorum\" (\"О пределах добра и зла\"), написанной Цицероном в 45 году н.э. Этот трактат по теории этики был очень популярен в эпоху Возрождения. Первая строка Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", происходит от одной из строк в разделе 1.10.32\n\r\n\rГде его взять?\n\rЕсть много вариантов Lorem Ipsum, но большинство из них имеет не всегда приемлемые модификации, например, юмористические вставки или слова, которые даже отдалённо не напоминают латынь. Если вам нужен Lorem Ipsum для серьёзного проекта, вы наверняка не хотите какой-нибудь шутки, скрытой в середине абзаца. Также все другие известные генераторы Lorem Ipsum используют один и тот же текст, который они просто повторяют, пока не достигнут нужный объём. Это делает предлагаемый здесь генератор единственным настоящим Lorem Ipsum генератором. Он использует словарь из более чем 200 латинских слов, а также набор моделей предложений. В результате сгенерированный Lorem Ipsum выглядит правдоподобно, не имеет повторяющихся абзацей или \"невозможных\" слов.";
        static void Main(string[] args) {

            Thread listnerResize = new Thread(ResizeListnerWork);
            Thread listnerUpdate = new Thread(UpdateListnerWork);

            listnerResize.Start();
            listnerUpdate.Start();
            Manage();

            listnerOn = false;
            listnerResize.Join();
            listnerUpdate.Join();
        }

        static void UpdateListnerWork() {
            listnerOn = true;
            
            while(listnerOn) {
                if (updateNeed) {
                    isUpdate = true;
                    Console.Clear();
                    ShowMenu();
                    ShowText(ref text);
                    updateNeed = false;
                    Thread.Sleep(200);
                    isUpdate = false;
                }

                Thread.Sleep(10);
            }
        }

        static void ResizeListnerWork() {
            listnerOn = true;
            int width = Console.WindowWidth;
            while (listnerOn) {
                if (!isUpdate && width != Console.WindowWidth) {
                    width = Console.WindowWidth;
                    updateNeed = true;
                }

                Thread.Sleep(10);
            }
        }

        static void Manage() {
            

            while(true) {
                updateNeed = true;
                var key = Console.ReadKey(true);

                switch(key.Key) {
                    case ConsoleKey.Q: return;
                }
            }
        }

        static void ShowMenu() {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < Console.WindowWidth - 1; i++)
                Console.Write(" ");
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            PrintMenuCommand("Q", "Выход");
            PrintMenuCommand("F", "Поиск");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }

        static void PrintMenuCommand(string key, string action) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(key);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" " + action + " ");
        }

        static void ShowText(ref string text) {
            int position = 0;
            while (position < text.Length) {
                Console.WriteLine(GetLine(ref text, ref position));
            }
        }

        static string GetLine(ref string text, ref int position) {
            int width = Console.WindowWidth - 1;
            int endLine = text.IndexOf('\n', position);
            endLine = (endLine == -1) ? text.Length : endLine;

            if (endLine - position >= width) {
                int lastSpace = endLine;

                // При обновлении иногда ArgumentOutOfRangeException
                try {
                    lastSpace = text.LastIndexOf(' ', position + width, width);
                } catch {}

                if (lastSpace == -1) {
                    lastSpace = text.IndexOf(' ', position + width, endLine - position - width);
                }

                if (lastSpace != -1) {
                    endLine = lastSpace;
                }
            }

            string line = text.Substring(position, endLine - position);
            if (endLine + 1 < text.Length && text[endLine + 1] == '\r') {
                ++endLine;
            }
            position = endLine + 1;

            return line;

        }
    }
}
