using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;

namespace Task_7._2._1 {
    class Program {
        enum InputStage {
            Escape,
            InputFind,
            InputReplace,
            OutputReplace,
            InputLoad,
            EndLoad,
            InputSave,
            EndSaveConfirmation,
            EndSave,
        }

        static void Main(string[] args) {
            Manage();
        }

        static void Manage() {
            string text = "";
            int[] highlightPositions = null;

            StringBuilder findStr = new StringBuilder();
            StringBuilder replaceStr = new StringBuilder();
            StringBuilder fileStr = new StringBuilder();
            bool ignoreCase = true;
            bool replace = false;
            bool needUpdateHighlight = true;
            bool wasLoadText = false;

            InputStage currentStage = InputStage.InputFind;

            while (true) {

                Console.Clear();

                if (currentStage == InputStage.Escape) {
                    return;
                }

                ShowMenu(currentStage, ignoreCase, replace);
                (int top, int left) = ShowRequest(findStr.ToString(), replaceStr.ToString(), fileStr.ToString(), replace, currentStage);
                Console.CursorVisible = true;

                if (currentStage == InputStage.EndLoad) {
                    try {
                        text = File.ReadAllText(fileStr.ToString());
                        Console.WriteLine("Файл успешно прочитан.");
                        wasLoadText = true;
                    } catch (DirectoryNotFoundException) {
                        Console.WriteLine($"Папка не найдена.");
                    } catch (FileNotFoundException) {
                        Console.WriteLine($"Файл не найден.");
                    } catch (System.Security.SecurityException) {
                        Console.WriteLine($"Недостаточно прав для доступа к файлу.");
                    } catch (Exception) {
                        Console.WriteLine($"Ошибка доступа к файлу.");
                    }

                    Console.CursorVisible = false;
                } else if (currentStage == InputStage.EndSave) {
                    try {
                        File.WriteAllText(fileStr.ToString(), text);
                        Console.WriteLine("Текст успешно записан в файл.");
                    } catch (DirectoryNotFoundException) {
                        Console.WriteLine($"Папка не найдена.");
                    } catch (System.Security.SecurityException) {
                        Console.WriteLine($"Недостаточно прав для записи в файл.");
                    } catch (Exception) {
                        Console.WriteLine($"Ошибка записи в файл.");
                    }

                    Console.CursorVisible = false;
                }

                Console.WriteLine();

                if (needUpdateHighlight || currentStage == InputStage.InputFind) {
                    if (currentStage == InputStage.InputFind) {
                        highlightPositions = Find(text, findStr.ToString(), ignoreCase);
                    } else if (currentStage == InputStage.OutputReplace) {
                        text = Replace(text, highlightPositions, findStr.ToString(), replaceStr.ToString(), ignoreCase);
                    }
                    needUpdateHighlight = false;
                }

                if (wasLoadText) {
                    ShowText(text, highlightPositions,
                        (currentStage == InputStage.OutputReplace) ? replaceStr.Length : findStr.Length,
                        (currentStage == InputStage.OutputReplace) ? ConsoleColor.Yellow : ConsoleColor.Green);
                } else {
                    Console.WriteLine("Текст не загружен.");
                }

                Console.CursorTop = top;
                Console.CursorLeft = left;

                Console.WindowTop = 0;

                var keyInfo = Console.ReadKey(true);
                var currentWriteField = fileStr;
                if (currentStage == InputStage.InputFind) {
                    currentWriteField = findStr;
                } else if (currentStage == InputStage.InputReplace || currentStage == InputStage.OutputReplace) {
                    currentWriteField = replaceStr;
                }

                if (HandleKeystroke(keyInfo, ref currentStage, ref replace, ref ignoreCase, currentWriteField)) {
                    if (currentStage == InputStage.InputFind) {
                        replaceStr.Clear();
                    } else if (currentStage == InputStage.OutputReplace) {
                        needUpdateHighlight = true;
                    } else if (currentStage == InputStage.InputLoad) {
                        findStr.Clear();
                        needUpdateHighlight = true;
                        if (fileStr.Length == 0) {
                            fileStr.Append(Directory.GetCurrentDirectory());
                        }
                    } else if (currentStage == InputStage.InputSave) {
                        if (wasLoadText) {
                            findStr.Clear();
                            needUpdateHighlight = true;
                        } else {
                            currentStage = InputStage.InputFind;
                        }
                    } else if (currentStage == InputStage.EndSaveConfirmation) {
                        if (!File.Exists(fileStr.ToString())) {
                            currentStage = InputStage.EndSave;
                        }
                    }
                }
            }
        }

        static string Replace(string text, int[] highlightPositions, string findStr, string replaceStr, bool ignoreCase) {
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

        static void ShowMenu(InputStage currentStage, bool ignoreCase, bool replace) {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            ShowMenuCommand("F2", "Учитывать регистр", !ignoreCase);
            ShowMenuCommand("F3", "Заменить", replace);
            ShowMenuCommand("F4", "Загрузить из файла", currentStage == InputStage.InputLoad || currentStage == InputStage.EndLoad);
            ShowMenuCommand("F5", "Сохранить в файл", currentStage == InputStage.InputSave || currentStage == InputStage.EndSaveConfirmation || currentStage == InputStage.EndSave);
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

        static (int, int) ShowRequest(string findStr, string replaceStr, string fileStr, bool replace, InputStage currentStage) {
            (int top, int left) cursorPos = (Console.CursorTop, Console.CursorLeft);

            if (currentStage == InputStage.InputLoad) {
                Console.WriteLine("Загрузить текст из файла:");
                Console.Write(fileStr);
                cursorPos = (Console.CursorTop, Console.CursorLeft);
                Console.WriteLine();
            } else if (currentStage == InputStage.InputSave || currentStage == InputStage.EndSaveConfirmation) {
                Console.WriteLine("Сохранить текст в файл:");
                Console.Write(fileStr);
                cursorPos = (Console.CursorTop, Console.CursorLeft);
                Console.WriteLine();
                if (currentStage == InputStage.EndSaveConfirmation) {
                    Console.Write("Файл существует. Перезаписать? д/н");
                    cursorPos = (Console.CursorTop, Console.CursorLeft);
                    Console.WriteLine();
                }
            } else if (currentStage == InputStage.InputFind || currentStage == InputStage.InputReplace || currentStage == InputStage.OutputReplace) {
                Console.WriteLine("Искать:");
                Console.Write(findStr);
                cursorPos = (Console.CursorTop, Console.CursorLeft);
                Console.WriteLine();

                if (replace) {
                    Console.WriteLine("Заменить на:");
                    Console.Write(replaceStr);
                    if (currentStage == InputStage.InputReplace || currentStage == InputStage.OutputReplace) {
                        cursorPos = (Console.CursorTop, Console.CursorLeft);
                    }
                    Console.WriteLine();
                }
            }

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
            if (currentStage == InputStage.EndLoad || currentStage == InputStage.EndSave) {
                currentStage = InputStage.InputFind;
                return true;
            }

            if (currentStage == InputStage.EndSaveConfirmation) {
                if (keyInfo.KeyChar == 'д') {
                    currentStage = InputStage.EndSave;
                    Console.Write('д');
                } else {
                    currentStage = InputStage.InputSave;
                    Console.Write('н');
                }

                return true;
            }

            if (keyInfo.Key == ConsoleKey.F2) {
                ignoreCase = !ignoreCase;
                return true;
            }

            if (keyInfo.Key == ConsoleKey.F3) {
                if (currentStage != InputStage.InputFind) {
                    currentStage = InputStage.InputFind;
                    stageWasChanged = true;
                }
                replace = !replace;
                return stageWasChanged;
            }

            if (keyInfo.Key == ConsoleKey.F4) {
                if (currentStage == InputStage.InputLoad || currentStage == InputStage.EndLoad) {
                    currentStage = InputStage.InputFind;
                } else {
                    currentStage = InputStage.InputLoad;

                }
                return true;
            }

            if (keyInfo.Key == ConsoleKey.F5) {
                if (currentStage == InputStage.InputSave || currentStage == InputStage.EndSave) {
                    currentStage = InputStage.InputFind;
                } else {
                    currentStage = InputStage.InputSave;
                }
                return true;
            }

            if (keyInfo.Key == ConsoleKey.Escape) {
                currentStage = InputStage.Escape;
                return true;
            }

            if (keyInfo.Key == ConsoleKey.Backspace) {
                if ((currentStage == InputStage.InputFind || currentStage == InputStage.InputReplace || currentStage == InputStage.InputLoad || currentStage == InputStage.InputSave) && writeField.Length > 0) {
                    writeField.Remove(writeField.Length - 1, 1);
                }
                return false;

            }

            if (keyInfo.Key == ConsoleKey.Enter) {
                if (replace) {
                    if (currentStage == InputStage.InputFind && writeField.Length > 0) {
                        currentStage = InputStage.InputReplace;
                        stageWasChanged = true;
                    } else if (currentStage == InputStage.InputReplace) {
                        currentStage = InputStage.OutputReplace;
                        stageWasChanged = true;
                    } else if (currentStage == InputStage.OutputReplace) {
                        currentStage = InputStage.InputFind;
                        stageWasChanged = true;
                    }
                }

                if (currentStage == InputStage.InputLoad && writeField.Length > 0) {
                    currentStage = InputStage.EndLoad;
                    stageWasChanged = true;
                } else if (currentStage == InputStage.InputSave && writeField.Length > 0) {
                    currentStage = InputStage.EndSaveConfirmation;
                    stageWasChanged = true;
                }

                return stageWasChanged;
            }

            if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsWhiteSpace(keyInfo.KeyChar) || char.IsPunctuation(keyInfo.KeyChar)) {
                writeField.Append(keyInfo.KeyChar);
                return false;
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
