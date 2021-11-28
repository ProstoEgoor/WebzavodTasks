using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Task_7._2._2 {

    abstract class ConsoleItem {

        protected ConsoleItem Prev { get; }
        ConsoleItem next;
        public ConsoleItem Next {
            get => next;
            set {
                next = value;
                if (next == Prev && Prev != null) {
                    Prev.Next = Prev;
                    Prev.NeedUpdate = true;
                }
            }
        }
        protected bool NeedUpdate { get; set; } = true;
        int WindowWidth { get; set; } = Console.WindowWidth;

        public ConsoleItem(ConsoleItem prev) {
            Prev = prev;
            Next = this;
        }

        public virtual void Show() {
            if (WindowWidth != Console.WindowWidth) {
                NeedUpdate = true;
                WindowWidth = Console.WindowWidth;
                OnResizeConsoleWindow();
            }

            if (NeedUpdate) {
                Console.Clear();
                ShowMenu();
            }
        }

        protected virtual void OnResizeConsoleWindow() { }

        public abstract bool HandleKeyStroke(ConsoleKeyInfo keyInfo, out bool needUpdate);

        protected abstract void ShowMenu();

        protected void ShowMenuCommand(string key, string action, bool active = false) {
            if (active) {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            } else {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(key);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" " + action);

            if (active) {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
            }

            Console.Write(" ");
            int cursorLeft = Console.CursorLeft;
            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
            Console.CursorLeft = cursorLeft;
            Console.ResetColor();
        }
    }
}
