using System;
using System.Text;

namespace Task_2._2._3 {
    class Program {
        enum Stage {
            Find,
            ReplaceInputF,
            ReplaceInputR,
            Replace,
            ReplaceOut
        }

        static void Main(string[] args) {
            Manage();
        }

        static void Manage() {
            string text = "Что такое Lorem Ipsum?\n\rLorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. Lorem Ipsum не только успешно пережил без заметных изменений пять веков, но и перешагнул в электронный дизайн. Его популяризации в новое время послужили публикация листов Letraset с образцами Lorem Ipsum в 60-х годах и, в более недавнее время, программы электронной вёрстки типа Aldus PageMaker, в шаблонах которых используется Lorem Ipsum.\n\r\n\rПочему он используется?\n\rДавно выяснено, что при оценке дизайна и композиции читаемый текст мешает сосредоточиться. Lorem Ipsum используют потому, что тот обеспечивает более или менее стандартное заполнение шаблона, а также реальное распределение букв и пробелов в абзацах, которое не получается при простой дубликации \"Здесь ваш текст..Здесь ваш текст..Здесь ваш текст..\" Многие программы электронной вёрстки и редакторы HTML используют Lorem Ipsum в качестве текста по умолчанию, так что поиск по ключевым словам \"lorem ipsum\" сразу показывает, как много веб-страниц всё ещё дожидаются своего настоящего рождения. За прошедшие годы текст Lorem Ipsum получил много версий. Некоторые версии появились по ошибке, некоторые - намеренно (например, юмористические варианты).\n\r\n\rОткуда он появился?\n\rМногие думают, что Lorem Ipsum - взятый с потолка псевдо-латинский набор слов, но это не совсем так. Его корни уходят в один фрагмент классической латыни 45 года н.э., то есть более двух тысячелетий назад. Ричард МакКлинток, профессор латыни из колледжа Hampden-Sydney, штат Вирджиния, взял одно из самых странных слов в Lorem Ipsum, \"consectetur\", и занялся его поисками в классической латинской литературе. В результате он нашёл неоспоримый первоисточник Lorem Ipsum в разделах 1.10.32 и 1.10.33 книги \"de Finibus Bonorum et Malorum\" (\"О пределах добра и зла\"), написанной Цицероном в 45 году н.э. Этот трактат по теории этики был очень популярен в эпоху Возрождения. Первая строка Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", происходит от одной из строк в разделе 1.10.32\n\r\n\rГде его взять?\n\rЕсть много вариантов Lorem Ipsum, но большинство из них имеет не всегда приемлемые модификации, например, юмористические вставки или слова, которые даже отдалённо не напоминают латынь. Если вам нужен Lorem Ipsum для серьёзного проекта, вы наверняка не хотите какой-нибудь шутки, скрытой в середине абзаца. Также все другие известные генераторы Lorem Ipsum используют один и тот же текст, который они просто повторяют, пока не достигнут нужный объём. Это делает предлагаемый здесь генератор единственным настоящим Lorem Ipsum генератором. Он использует словарь из более чем 200 латинских слов, а также набор моделей предложений. В результате сгенерированный Lorem Ipsum выглядит правдоподобно, не имеет повторяющихся абзацей или \"невозможных\" слов.";
            byte[] bgColor = new byte[0];

            StringBuilder findStr = new StringBuilder();
            StringBuilder replaceStr = new StringBuilder();
            bool ignoreCase = true;

            Stage stage = Stage.Find;

            int windowTopPrev = 0;

            while (true) {

                Console.Clear();
                ShowMenu(stage, ignoreCase);
                (int left, int top) cursorPos = ShowRequest(findStr.ToString(), replaceStr.ToString(), stage, ignoreCase);
                FindColor(ref text, ref bgColor, findStr.ToString(), ignoreCase, stage);

                if (stage == Stage.Replace) {
                    Replace(ref text, ref bgColor, findStr.ToString(), replaceStr.ToString(), ignoreCase);
                    stage = Stage.ReplaceOut;
                    continue;
                }

                ShowText(ref text, bgColor);

                Console.CursorTop = cursorPos.top;
                Console.CursorLeft = cursorPos.left;

                Console.WindowTop = windowTopPrev;
                windowTopPrev = 0;

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Q) {
                    return;
                } else if (key.Key == ConsoleKey.Backspace) {
                    if (findStr.Length > 0 && (stage == Stage.Find || stage == Stage.ReplaceInputF)) {
                        findStr.Remove(findStr.Length - 1, 1);
                    } else if (stage == Stage.ReplaceInputR) {
                        if (replaceStr.Length > 0) {
                            replaceStr.Remove(replaceStr.Length - 1, 1);
                        } else {
                            stage = Stage.ReplaceInputF;
                        }
                    }

                } else if (key.Key == ConsoleKey.Enter) {
                    if (findStr.Length > 0 && stage == Stage.ReplaceInputF) {
                        stage = Stage.ReplaceInputR;
                    } else if (stage == Stage.ReplaceInputR) {
                        stage = Stage.Replace;
                    }

                } else if (key.KeyChar != 0) {
                    if (stage == Stage.Find || stage == Stage.ReplaceInputF) {
                        findStr.Append(key.KeyChar);
                    } else if (stage == Stage.ReplaceInputR) {
                        replaceStr.Append(key.KeyChar);
                    }

                } else if (key.Key == ConsoleKey.F2) {
                    ignoreCase = !ignoreCase;
                    windowTopPrev = Console.WindowTop;

                } else if (key.Key == ConsoleKey.F3) {
                    if (stage != Stage.Find) {
                        stage = Stage.Find;
                    } else {
                        stage = Stage.ReplaceInputF;
                    }
                }

            }
        }

        private static void Replace(ref string text, ref byte[] bgColor, string findStr, string replaceStr, bool ignoreCase) {
            StringBuilder sb = new StringBuilder(text);

            int offset = 0;

            /*sb.Replace(v1, v2);*/

            for (int i = 0; i < bgColor.Length; i++) {
                if (bgColor[i] != 0) {
                    sb.Remove(i + offset, findStr.Length);
                    sb.Insert(i + offset, replaceStr);

                    i += findStr.Length;
                    offset += replaceStr.Length - findStr.Length;
                }
            }
            text = sb.ToString();
        }

        static (int, int) ShowRequest(string findStr, string replaceStr, Stage stage, bool ignoreCase) {
            Console.WriteLine($"Искать: {findStr}");
            (int left, int top) cursorPos = (findStr.Length + 8, 1);

            if (stage != Stage.Find) {
                Console.WriteLine($"Заменить на: {replaceStr}");
            }

            if (stage == Stage.ReplaceInputR || stage == Stage.Replace) {
                cursorPos = (replaceStr.Length + 13, 2);
            }

            Console.WriteLine();
            Console.WriteLine();

            return cursorPos;

        }

        static void FindColor(ref string text, ref byte[] bgColor, string findStr, bool ignoreCase, Stage stage) {
            if (bgColor == null || bgColor.Length != text.Length) {
                bgColor = new byte[text.Length];
            } else {
                for (int i = 0; i < bgColor.Length; i++) {
                    bgColor[i] = 0;
                }
            }



            if (findStr == "") {
                return;
            }

            int pos = 0;
            while (pos != -1) {
                if (ignoreCase) {
                    pos = text.IndexOf(findStr, pos, StringComparison.OrdinalIgnoreCase);
                } else {
                    pos = text.IndexOf(findStr, pos);
                }

                if (pos != -1) {
                    for (int i = 0; i < findStr.Length; i++) {
                        bgColor[pos + i] = (stage == Stage.ReplaceOut) ? (byte)ConsoleColor.Yellow : (byte)ConsoleColor.Green;
                    }

                    pos += findStr.Length;
                }
            }
        }

        static void ShowMenu(Stage stage, bool ignoreCase) {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            PrintMenuCommand("Q", "Выход");
            PrintMenuCommand("F2", "Учитывать регистр", !ignoreCase);
            PrintMenuCommand("F3", "Заменить", stage != Stage.Find);
            Console.WriteLine();
            Console.ResetColor();
        }

        static void PrintMenuCommand(string key, string action, bool active = false) {
            var bg = Console.BackgroundColor;
            if (active) {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(key);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" " + action);

            Console.BackgroundColor = bg;
            Console.Write(" ");
        }

        static void ShowText(ref string text, byte[] bgColor) {
            int startPos = 0;
            int nextStartPos = 0;
            while (startPos < text.Length) {
                ShowLine(ref text, startPos, GetEndLine(ref text, ref nextStartPos), ref bgColor);
                startPos = nextStartPos;
            }
        }

        static void ShowLine(ref string text, int start, int end, ref byte[] bgColor) {
            for (int i = start; i < end; i++) {
                if (bgColor[i] != 0) {
                    Console.BackgroundColor = (ConsoleColor)bgColor[i];
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.Write(text[i]);

                if (bgColor[i] != 0) {
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }

        static int GetEndLine(ref string text, ref int startPos) {
            int width = Console.WindowWidth - 1;
            int endPos = text.IndexOf('\n', startPos);
            endPos = (endPos == -1) ? text.Length : endPos;

            if (endPos - startPos >= width) {
                int neerSpacePos = text.LastIndexOf(' ', startPos + width, width);

                if (neerSpacePos == -1) {
                    neerSpacePos = text.IndexOf(' ', startPos + width, endPos - startPos - width);
                }

                if (neerSpacePos != -1) {
                    endPos = neerSpacePos;
                }
            }

            startPos = endPos + 1;

            if (startPos < text.Length && text[startPos] == '\r') {
                ++startPos;
            }

            return endPos;
        }
    }
}
