using System;
using System.Collections.Generic;
using System.Text;

namespace Task_7._2._2 {
    class WriteField {
        public int Width { get; set; } = Console.WindowWidth;
        StringBuilder text = new StringBuilder();
        public string Text {
            get => text.ToString();
            set {
                text = new StringBuilder(value);
                textCursor = text.Length;
            }
        }

        public (int Top, int Left) ConsoleCursor;

        int Left;
        public bool Area { get; set; }

        List<(int start, int length)> LinePositions { get; } = new List<(int start, int length)>();
        int startCursorTop;
        int textCursor;

        public void Show() {
            int start = 0;
            string text = Text;
            LinePositions.Clear();
            startCursorTop = Console.CursorTop;
            while (TryGetLine(text, Width, start, out int end, out int newStart)) {
                Console.Write(text[start..end]);
                LinePositions.Add((start, end - start));
                if (end < text.Length) {
                    Console.WriteLine();
                }
                start = newStart;
            }

            if (Console.CursorTop - startCursorTop >= LinePositions.Count) {
                LinePositions.Add((start, 0));
            }

            for (int i = 0; i < LinePositions.Count; i++) {
                if (LinePositions[i].start <= textCursor && textCursor <= LinePositions[i].start + LinePositions[i].length) {
                    ConsoleCursor.Top = startCursorTop + i;
                    ConsoleCursor.Left = textCursor - LinePositions[i].start;
                    Left = ConsoleCursor.Left;
                }
            }
        }

        public bool HandleKeystroke(ConsoleKeyInfo keyInfo, out bool needUpdate) {
            needUpdate = false;
            bool react = false;

            int top = Console.CursorTop - startCursorTop;
            int left = Console.CursorLeft;

            if (keyInfo.Key == ConsoleKey.LeftArrow && textCursor > 0) {
                if (left > 0) {
                    Console.CursorLeft--;
                } else {
                    Console.CursorTop--;
                    Console.CursorLeft = LinePositions[--top].length;
                }
                Left = Console.CursorLeft;
                textCursor = LinePositions[top].start + Console.CursorLeft;
                react = true;
            }

            if (keyInfo.Key == ConsoleKey.RightArrow && textCursor < text.Length) {
                if (left < LinePositions[top].length) {
                    Console.CursorLeft++;
                } else {
                    Console.CursorTop++;
                    top++;
                    Console.CursorLeft = 0;
                }

                Left = Console.CursorLeft;
                textCursor = LinePositions[top].start + Console.CursorLeft;
                react = true;
            }

            if (keyInfo.Key == ConsoleKey.UpArrow && top > 0) {
                if (Console.WindowTop == Console.CursorTop) {
                    Console.WindowTop--;
                }

                Console.CursorTop--;
                Console.CursorLeft = Math.Min(Left, LinePositions[--top].length);
                textCursor = LinePositions[top].start + Console.CursorLeft;
                react = true;
            }

            if (keyInfo.Key == ConsoleKey.DownArrow && top + 1 < LinePositions.Count) {
                if (Console.WindowTop + Console.WindowHeight - 1 == Console.CursorTop) {
                    Console.WindowTop++;
                }

                Console.CursorTop++;
                Console.CursorLeft = Math.Min(Left, LinePositions[++top].length);
                textCursor = LinePositions[top].start + Console.CursorLeft;
                react = true;
            }

            if (keyInfo.Key == ConsoleKey.Backspace && textCursor > 0) {
               int removeLength = 1;
                if (textCursor > 1 && text[textCursor - 1] == '\n' && text[textCursor - 2] == '\r') {
                    removeLength = 2;
                }

                if (left >= removeLength) {
                    Console.CursorLeft -= removeLength;
                    Console.Write(text.ToString(textCursor, LinePositions[top].length - left));
                    Console.Write(new string(' ', removeLength));
                    Console.CursorLeft = left - removeLength;
                    LinePositions[top] = (LinePositions[top].start, LinePositions[top].length - removeLength);
                    for (int i = top + 1; i < LinePositions.Count; i++) {
                        LinePositions[i] = (LinePositions[i].start - removeLength, LinePositions[i].length);
                    }
                } else {
                    needUpdate = true;
                }
                textCursor -= removeLength;
                text.Remove(textCursor, removeLength);

                TryGetLine(Text, Width, LinePositions[top].start, out int end, out int newStart);
                if (end - LinePositions[top].start != LinePositions[top].length) {
                    needUpdate = true;
                }

                react = true;
            }

            if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsPunctuation(keyInfo.KeyChar) || keyInfo.KeyChar == ' ') {
                if (LinePositions[top].length + 1 < Width) {
                    Console.Write(keyInfo.KeyChar);
                    Console.Write(text.ToString(textCursor, LinePositions[top].length - left));
                    Console.CursorLeft = left + 1;
                    LinePositions[top] = (LinePositions[top].start, LinePositions[top].length + 1);
                    for (int i = top + 1; i < LinePositions.Count; i++) {
                        LinePositions[i] = (LinePositions[i].start + 1, LinePositions[i].length);
                    }
                } else {
                    needUpdate = true;
                }

                text.Insert(textCursor, keyInfo.KeyChar);
                textCursor++;
                react = true;
            }

            if (keyInfo.Key == ConsoleKey.Enter && Area) {
                text.Insert(textCursor, "\r\n");
                textCursor += 2;
                needUpdate = true;
                react = true;
            }

            ConsoleCursor.Top = Console.CursorTop;
            ConsoleCursor.Left = Console.CursorLeft;
            return react;
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
