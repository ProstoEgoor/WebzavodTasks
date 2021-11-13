using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Task_7._2._1 {
    class Program {
        enum InputStage {
            Escape,
            InputF,
            InputR,
            OutputR
        }

        static void Main(string[] args) {
            Manage();
        }

        static void Manage() {
            string text = "Что такое Lorem Ipsum?\r\nLorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. Lorem Ipsum не только успешно пережил без заметных изменений пять веков, но и перешагнул в электронный дизайн. Его популяризации в новое время послужили публикация листов Letraset с образцами Lorem Ipsum в 60-х годах и, в более недавнее время, программы электронной вёрстки типа Aldus PageMaker, в шаблонах которых используется Lorem Ipsum.\r\n\r\nПочему он используется?\r\nДавно выяснено, что при оценке дизайна и композиции читаемый текст мешает сосредоточиться. Lorem Ipsum используют потому, что тот обеспечивает более или менее стандартное заполнение шаблона, а также реальное распределение букв и пробелов в абзацах, которое не получается при простой дубликации \"Здесь ваш текст..Здесь ваш текст..Здесь ваш текст..\" Многие программы электронной вёрстки и редакторы HTML используют Lorem Ipsum в качестве текста по умолчанию, так что поиск по ключевым словам \"lorem ipsum\" сразу показывает, как много веб-страниц всё ещё дожидаются своего настоящего рождения. За прошедшие годы текст Lorem Ipsum получил много версий. Некоторые версии появились по ошибке, некоторые - намеренно (например, юмористические варианты).\r\n\r\nОткуда он появился?\r\nМногие думают, что Lorem Ipsum - взятый с потолка псевдо-латинский набор слов, но это не совсем так. Его корни уходят в один фрагмент классической латыни 45 года н.э., то есть более двух тысячелетий назад. Ричард МакКлинток, профессор латыни из колледжа Hampden-Sydney, штат Вирджиния, взял одно из самых странных слов в Lorem Ipsum, \"consectetur\", и занялся его поисками в классической латинской литературе. В результате он нашёл неоспоримый первоисточник Lorem Ipsum в разделах 1.10.32 и 1.10.33 книги \"de Finibus Bonorum et Malorum\" (\"О пределах добра и зла\"), написанной Цицероном в 45 году н.э. Этот трактат по теории этики был очень популярен в эпоху Возрождения. Первая строка Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", происходит от одной из строк в разделе 1.10.32\r\n\r\nГде его взять?\r\nЕсть много вариантов Lorem Ipsum, но большинство из них имеет не всегда приемлемые модификации, например, юмористические вставки или слова, которые даже отдалённо не напоминают латынь. Если вам нужен Lorem Ipsum для серьёзного проекта, вы наверняка не хотите какой-нибудь шутки, скрытой в середине абзаца. Также все другие известные генераторы Lorem Ipsum используют один и тот же текст, который они просто повторяют, пока не достигнут нужный объём. Это делает предлагаемый здесь генератор единственным настоящим Lorem Ipsum генератором. Он использует словарь из более чем 200 латинских слов, а также набор моделей предложений. В результате сгенерированный Lorem Ipsum выглядит правдоподобно, не имеет повторяющихся абзацей или \"невозможных\" слов.";
            int[] highlightPositions = null;

            StringBuilder findStr = new StringBuilder();
            StringBuilder replaceStr = new StringBuilder();
            bool ignoreCase = true;
            bool replace = false;
            bool needUpdateHighlight = true;

            InputStage currentStage = InputStage.InputF;

            while (true) {

                Console.Clear();

                if (currentStage == InputStage.Escape) {
                    return;
                }

                ShowMenu(ignoreCase, replace);
                (int top, int left) = ShowRequest(findStr.ToString(), replaceStr.ToString(), replace, currentStage);

                if (needUpdateHighlight || currentStage == InputStage.InputF) {
                    if (currentStage == InputStage.InputF) {
                        highlightPositions = Find(text, findStr.ToString(), ignoreCase);
                    } else if (currentStage == InputStage.OutputR) {
                        text = Replace(text, highlightPositions, findStr.ToString(), replaceStr.ToString(), ignoreCase);
                    }
                    needUpdateHighlight = false;
                }

                ShowText(text, highlightPositions,
                    (currentStage == InputStage.OutputR) ? replaceStr.Length : findStr.Length,
                    (currentStage == InputStage.OutputR) ? ConsoleColor.Yellow : ConsoleColor.Green);

                Console.CursorTop = top;
                Console.CursorLeft = left;

                Console.WindowTop = 0;

                var keyInfo = Console.ReadKey(true);
                var currentWriteField = (currentStage == InputStage.InputF) ? findStr : replaceStr;

                if (HandleKeystroke(keyInfo, ref currentStage, ref replace, ref ignoreCase, currentWriteField)) {
                    if (currentStage == InputStage.InputF) {
                        replaceStr.Clear();
                    } else if (currentStage == InputStage.OutputR) {
                        needUpdateHighlight = true;
                    }
                }
            }
        }

        private static string Replace(string text, int[] highlightPositions, string findStr, string replaceStr, bool ignoreCase) {
            Regex regex = new Regex(Regex.Escape(findStr), (RegexOptions)((int)RegexOptions.IgnoreCase * Convert.ToInt32(ignoreCase)));

            int offset = replaceStr.Length - findStr.Length;

            for (int i = 0; i < highlightPositions.Length; i++) {
                highlightPositions[i] += offset * i;
            }

            return regex.Replace(text, replaceStr);
        }

        static int[] Find(string text, string findStr, bool ignoreCase) {
            if (findStr.Length == 0) {
                return new int[0];
            }

            Regex regex = new Regex(Regex.Escape(findStr), (RegexOptions)((int)RegexOptions.IgnoreCase * Convert.ToInt32(ignoreCase)));

            MatchCollection matches = regex.Matches(text);

            return matches.Select(match => match.Index).ToArray();
        }

        static void ShowMenu(bool ignoreCase, bool replace) {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            ShowMenuCommand("F2", "Учитывать регистр", !ignoreCase);
            ShowMenuCommand("F3", "Заменить", replace);
            ShowMenuCommand("Esc", "Выход");
            Console.WriteLine();
            Console.ResetColor();
        }

        static void ShowMenuCommand(string key, string action, bool active = false) {
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

        static (int, int) ShowRequest(string findStr, string replaceStr, bool replace, InputStage currentStage) {
            Console.WriteLine("Искать:");
            Console.Write(findStr);
            (int top, int left) cursorPos = (Console.CursorTop, Console.CursorLeft);
            Console.WriteLine();

            if (replace) {
                Console.WriteLine("Заменить на:");
                Console.Write(replaceStr);
                if (currentStage == InputStage.InputR || currentStage == InputStage.OutputR) {
                    cursorPos = (Console.CursorTop, Console.CursorLeft);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            return cursorPos;
        }

        static void ShowText(string text, int[] highlightPositions, int lengthOfHighlight, ConsoleColor bgColor) {
            int start = 0;
            int highlightPos = 0;
            while (TryGetLine(text, Console.WindowWidth - 1, start, out int end, out int newStart)) {
                ShowLine(text, start, end, ref highlightPos, highlightPositions, lengthOfHighlight, bgColor);
                start = newStart;
            }
        }

        static void ShowLine(string text, int start, int end, ref int highlightPos, int[] highlightPositions, int lengthOfHighlight, ConsoleColor bgColor) {
            Console.Write(text[start..end]);

            if (highlightPos > 0 && highlightPositions[highlightPos - 1] > start - lengthOfHighlight) {
                --highlightPos;
            }

            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = ConsoleColor.Black;

            while (highlightPos < highlightPositions.Length && highlightPositions[highlightPos] >= start - lengthOfHighlight && highlightPositions[highlightPos] < end) {
                int highlightStart = Math.Max(start, highlightPositions[highlightPos]);
                int highlightEnd = Math.Min(end, highlightPositions[highlightPos] + lengthOfHighlight);
                Console.CursorLeft = highlightStart - start;
                Console.Write(text[highlightStart..highlightEnd]);
                ++highlightPos;
            }

            Console.ResetColor();

            Console.WriteLine();
        }

        static bool HandleKeystroke(ConsoleKeyInfo keyInfo, ref InputStage currentStage, ref bool replace, ref bool ignoreCase, StringBuilder writeField) {
            bool stageWasChanged = false;
            if (keyInfo.Key == ConsoleKey.F2) {

                ignoreCase = !ignoreCase;

            } else if (keyInfo.Key == ConsoleKey.F3) {

                if (currentStage != InputStage.InputF) {
                    currentStage = InputStage.InputF;
                    stageWasChanged = true;
                }
                replace = !replace;

            } else if (keyInfo.Key == ConsoleKey.Escape) {

                currentStage = InputStage.Escape;
                stageWasChanged = true;

            } else if (keyInfo.Key == ConsoleKey.Backspace) {

                if ((currentStage == InputStage.InputF || currentStage == InputStage.InputR) && writeField.Length > 0) {
                    writeField.Remove(writeField.Length - 1, 1);
                }

            } else if (keyInfo.Key == ConsoleKey.Enter) {

                if (replace) {
                    if (currentStage == InputStage.InputF && writeField.Length > 0) {
                        currentStage = InputStage.InputR;
                        stageWasChanged = true;
                    } else if (currentStage == InputStage.InputR) {
                        currentStage = InputStage.OutputR;
                        stageWasChanged = true;
                    } else if (currentStage == InputStage.OutputR) {
                        currentStage = InputStage.InputF;
                        stageWasChanged = true;
                    }
                }

            } else if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsWhiteSpace(keyInfo.KeyChar) || char.IsPunctuation(keyInfo.KeyChar)) {

                writeField.Append(keyInfo.KeyChar);

            }

            return stageWasChanged;
        }


        static bool TryGetLine(string text, int width, int start, out int end, out int newStart) {
            if (width <= 1) {
                throw new ArgumentException("Width must be larger than 1.");
            }

            if (start >= text.Length) {
                end = start;
                newStart = end;
                return false;
            }

            end = text.IndexOfAny(new char[] { '\n', '\r' }, start, Math.Min(width, text.Length - start));

            if (end != -1) {
                newStart = (text[end] == '\r') ? end + 2 : end + 1;
            } else {
                newStart = end;
            }

            if (start + width >= text.Length) {
                if (end == -1) {
                    end = text.Length;
                    newStart = end;
                }
            } else {
                if (end == -1) {
                    end = text.LastIndexOf(' ', start + width, width);
                    newStart = end + 1;
                }
                if (end == -1) {
                    end = start + width;
                    newStart = end;
                }
            }
            return true;
        }
    }
}
