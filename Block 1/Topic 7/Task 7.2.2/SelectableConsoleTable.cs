using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Task_7._2._2 {

    enum Alignment {
        Left = -1,
        Center = 0,
        Right = 1
    };
    class SelectableConsoleTable {
        public int ColumnsCount { get; }
        public int[] ColumnsWidth { get; }
        public Alignment[] ColumnsAlign { get; }
        List<string[]> Rows { get; } = new List<string[]>();
        public char Separator { get; set; } = ' ';

        int position = 0;
        public int Position {
            get => position;
            set {
                position = value;
                if (position >= Rows.Count - 1) {
                    position = Rows.Count - 2;
                }
                if (position < 0) {
                    position = 0;
                }
            }
        }

        int StartCursorTop { get; set; }
        int RowHeight => (int)Math.Ceiling((ColumnsWidth.Sum() + ColumnsCount - 1) / (double)Console.WindowWidth);

        public SelectableConsoleTable(int[] columnsWidth, Alignment[] columnsAlign, string[] head) {
            if (columnsWidth.Length != columnsAlign.Length || columnsWidth.Length != head.Length) {
                throw new ArgumentException("The lengths of the arrays do not match.");
            }
            ColumnsCount = columnsWidth.Length;
            ColumnsWidth = new int[ColumnsCount];
            ColumnsAlign = new Alignment[ColumnsCount];
            Array.Copy(columnsWidth, ColumnsWidth, ColumnsCount);
            Array.Copy(columnsAlign, ColumnsAlign, ColumnsCount);
            Rows.Add(head);
        }

        public void AddRow(params string[] row) {
            string[] _row = new string[ColumnsCount];
            for (int i = 0; i < _row.Length; i++) {
                if (i < row.Length) {
                    _row[i] = row[i];
                } else {
                    _row[i] = "";
                }
            }
            Rows.Add(_row);
        }

        public void ClearRows() {
            Rows.RemoveRange(1, Rows.Count - 1);
            Position = 0;
        }

        public void Show() {
            StartCursorTop = Console.CursorTop;
            for (int i = 0; i < Rows.Count; i++) {
                ShowRow(i, i - 1 == Position);
            }
        }

        void ShowRow(int position, bool selected) {
            if (selected) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            int cursorTop;
            for (int i = 0; i < ColumnsCount; i++) {
                if (Console.CursorLeft != 0 && ColumnsWidth[i] > Console.WindowWidth - Console.CursorLeft) {
                    cursorTop = Console.CursorTop + 1;
                    Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
                    Console.CursorTop = cursorTop;
                    Console.CursorLeft = 0;
                }

                string cell = Rows[position][i].Length > ColumnsWidth[i] ? Rows[position][i].Substring(0, ColumnsWidth[i]) : Rows[position][i];

                if (ColumnsAlign[i] == Alignment.Center) {
                    int left = (ColumnsWidth[i] - cell.Length) / 2;
                    int right = ColumnsWidth[i] - cell.Length - left;

                    Console.Write(new string(' ', left));
                    Console.Write(cell);
                    Console.Write(new string(' ', right));
                } else {
                    Console.Write(string.Format($"{{0,{ColumnsWidth[i] * (int)ColumnsAlign[i]}}}", cell));
                }

                if (i + 1 < ColumnsCount) {
                    Console.Write(Separator);
                }
            }
            cursorTop = Console.CursorTop + 1;
            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
            Console.CursorTop = cursorTop;
            Console.CursorLeft = 0;

            if (selected) {
                Console.ResetColor();
            }
        }

        public bool HandleKeystroke(ConsoleKeyInfo keyInfo, out bool needUpdate) {
            needUpdate = true;
            bool react = false;
            int prevPosition = Position;

            if (keyInfo.Key == ConsoleKey.UpArrow) {
                Position--;
                needUpdate = false;
                react = true;
            } else if (keyInfo.Key == ConsoleKey.DownArrow) {
                Position++;
                needUpdate = false;
                react = true;
            }

            if (prevPosition != Position) {
                Console.CursorTop = StartCursorTop + (prevPosition + 1) * RowHeight;
                Console.CursorLeft = 0;
                ShowRow(prevPosition + 1, false);

                Console.CursorTop = StartCursorTop + (Position + 1) * RowHeight;
                Console.CursorLeft = 0;
                ShowRow(Position + 1, true);
            }

            return react;
        }
    }
}
