using System;
using System.Text;

namespace Task_2._2._3 {
    class Program {
        enum ReplaceStage {
            InputF,
            InputR,
            Output
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
            bool replace = false;
            bool needUpdateBg = true;

            ReplaceStage stage = ReplaceStage.InputF;

            int windowTopPrev = 0;

            int count = 0;

            while (true) {

                Console.Clear();
                ShowMenu(ignoreCase, replace);
                (int left, int top) cursorPos = ShowRequest(findStr.ToString(), replaceStr.ToString(), replace, stage);

                if (needUpdateBg) {
                    if (stage != ReplaceStage.Output) {
                        count = Find(ref text, ref bgColor, findStr.ToString(), ignoreCase);
                    } else {
                        Replace(ref text, ref bgColor, findStr.ToString(), replaceStr.ToString(), count);
                    }
                    needUpdateBg = false;
                }

                ShowText(ref text, bgColor);

                Console.CursorTop = cursorPos.top;
                Console.CursorLeft = cursorPos.left;

                Console.WindowTop = windowTopPrev;
                windowTopPrev = 0;

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace) {

                    if (findStr.Length > 0 && (!replace || stage == ReplaceStage.InputF)) {
                        findStr.Remove(findStr.Length - 1, 1);
                        needUpdateBg = true;
                    } else if (stage == ReplaceStage.InputR) {
                        if (replaceStr.Length > 0) {
                            replaceStr.Remove(replaceStr.Length - 1, 1);
                        } else {
                            stage = ReplaceStage.InputF;
                        }
                    }

                } else if (key.Key == ConsoleKey.Enter) {
                    if (!replace) {
                        continue;
                    }

                    if (stage == ReplaceStage.InputF && findStr.Length > 0) {
                        stage = ReplaceStage.InputR;
                    } else if (stage == ReplaceStage.InputR) {
                        stage = ReplaceStage.Output;
                        needUpdateBg = true;
                    } else if (stage == ReplaceStage.Output) {
                        stage = ReplaceStage.InputF;
                        findStr.Clear();
                        replaceStr.Clear();
                        needUpdateBg = true;
                    }

                } else if (key.Key == ConsoleKey.F2) {
                    windowTopPrev = Console.WindowTop;

                    if (stage != ReplaceStage.Output) {
                        ignoreCase = !ignoreCase;
                        needUpdateBg = true;
                    }

                } else if (key.Key == ConsoleKey.F3) {

                    replaceStr.Clear();
                    stage = ReplaceStage.InputF;
                    replace = !replace;
                    needUpdateBg = true;

                } else if (key.Key == ConsoleKey.Escape) {
                    return;
                } else if (key.KeyChar != 0) {

                    if (!replace || stage == ReplaceStage.InputF) {
                        findStr.Append(key.KeyChar);
                        needUpdateBg = true;
                    } else if (stage == ReplaceStage.InputR) {
                        replaceStr.Append(key.KeyChar);
                    }

                }

                if (findStr.Length > Console.WindowWidth) {
                    findStr.Remove(Console.WindowWidth, findStr.Length - Console.WindowWidth);
                }

                if (replaceStr.Length > Console.WindowWidth) {
                    replaceStr.Remove(Console.WindowWidth, replaceStr.Length - Console.WindowWidth);
                }
            }
        }

        private static void Replace(ref string text, ref byte[] bgColor, string findStr, string replaceStr, int count) {
            StringBuilder sb = new StringBuilder(text);

            int oneOffset = replaceStr.Length - findStr.Length;
            int offset = 0;

            byte[] newBgColor = new byte[bgColor.Length + oneOffset * count];

            /*sb.Replace(v1, v2);*/

            for (int i = 0; i < bgColor.Length; i++) {
                if (bgColor[i] != 0) {
                    sb.Remove(i + offset, findStr.Length);
                    sb.Insert(i + offset, replaceStr);

                    for (int j = 0; j < replaceStr.Length; j++) {
                        newBgColor[i + offset + j] = (byte)ConsoleColor.Yellow;
                    }

                    i += findStr.Length;
                    offset += replaceStr.Length - findStr.Length;
                }
            }

            bgColor = newBgColor;
            text = sb.ToString();
        }

        static int Find(ref string text, ref byte[] bgColor, string findStr, bool ignoreCase) {
            if (bgColor == null || bgColor.Length != text.Length) {
                bgColor = new byte[text.Length];
            } else {
                for (int i = 0; i < bgColor.Length; i++) {
                    bgColor[i] = 0;
                }
            }

            if (findStr == "") {
                return 0;
            }

            int count = 0;

            int pos = 0;
            while (pos != -1) {
                if (ignoreCase) {
                    pos = text.IndexOf(findStr, pos, StringComparison.OrdinalIgnoreCase);
                } else {
                    pos = text.IndexOf(findStr, pos);
                }

                if (pos != -1) {
                    for (int i = 0; i < findStr.Length; i++) {
                        bgColor[pos + i] = (byte)ConsoleColor.Green;
                    }

                    pos += findStr.Length;
                    ++count;
                }
            }

            return count;
        }

        static void ShowMenu(bool ignoreCase, bool replace) {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            PrintMenuCommand("F2", "Учитывать регистр", !ignoreCase);
            PrintMenuCommand("F3", "Заменить", replace);
            PrintMenuCommand("Esc", "Выход");
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

        static (int, int) ShowRequest(string findStr, string replaceStr, bool replace, ReplaceStage stage) {
            Console.WriteLine("Искать:");
            Console.WriteLine(findStr);
            (int left, int top) cursorPos = (findStr.Length, 2);

            if (replace) {
                Console.WriteLine("Заменить на:");
                Console.WriteLine(replaceStr);

                if (stage == ReplaceStage.InputR || stage == ReplaceStage.Output) {
                    cursorPos = (replaceStr.Length, 4);
                }
            }

            Console.WriteLine();

            return cursorPos;
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
