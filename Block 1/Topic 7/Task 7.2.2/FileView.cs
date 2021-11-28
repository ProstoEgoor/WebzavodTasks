using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Task_7._2._2 {
    enum FileState {
        View,
        Save,
        Message
    }

    class FileView : ConsoleItem {
        string DirectoryName { get; }
        string Name { get; set; }
        string Message { get; set; } = "";

        FileState State { get; set; } = FileState.View;
        WriteField WriteField { get; } = new WriteField() { Area = true };
        WriteField NameField { get; } = new WriteField();
        public FileView(ConsoleItem prev, string directoryName, string name = null) : base(prev) {
            DirectoryName = directoryName;
            Name = name ?? "";
            if (name != null) {
                string path = Path.Combine(DirectoryName, Name);
                if (File.Exists(path)) {
                    FileInfo FileInfo = new FileInfo(path);
                    using StreamReader streamReader = FileInfo.OpenText();
                    WriteField.Text = streamReader.ReadToEnd();
                } else {
                    throw new ArgumentException("File path isn't correct.");
                }
            }
        }
        public override void Show() {
            base.Show();

            if (NeedUpdate) {
                if (State == FileState.View) {
                    Console.CursorVisible = true;
                    WriteField.Show();
                    Console.CursorTop = WriteField.ConsoleCursor.Top;
                    Console.CursorLeft = WriteField.ConsoleCursor.Left;
                } else if (State == FileState.Save) {
                    Console.CursorVisible = true;
                    Console.WriteLine("Сохранить файл по пути: ");
                    NameField.Show();
                    Console.CursorTop = NameField.ConsoleCursor.Top;
                    Console.CursorLeft = NameField.ConsoleCursor.Left;
                } else if (State == FileState.Message) {
                    Console.CursorVisible = true;
                    Console.Write($"{Message} Нажмите чтобы продолжить...");
                }
            }
        }

        public override bool HandleKeyStroke(ConsoleKeyInfo keyInfo, out bool needUpdate) {
            bool react = false;
            needUpdate = false;

            if (State == FileState.View) {
                if (keyInfo.Key == ConsoleKey.Escape) {
                    Next = Prev;
                } else if (keyInfo.Key == ConsoleKey.F1) {
                    State = FileState.Save;
                    needUpdate = true;
                } else {
                    react = WriteField.HandleKeystroke(keyInfo, out needUpdate);
                }
            } else if (State == FileState.Save) {
                if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.F1) {
                    State = FileState.View;
                    needUpdate = true;
                } else if (keyInfo.Key == ConsoleKey.Enter && NameField.Text.Length > 0) {
                    string path = Path.Combine(DirectoryName, NameField.Text);
                    if (!Path.HasExtension(path)) {
                        path = Path.ChangeExtension(path, "txt");
                    }
                    try {
                        FileInfo fileInfo = new FileInfo(path);
                        using StreamWriter stream = fileInfo.CreateText();
                        stream.Write(WriteField.Text);
                        Name = NameField.Text;
                        Message = $"Файл {fileInfo.Name} успешно сохранен.";
                    } catch (Exception) {
                        Message = "Невозможно сохранить файл.";
                    }
                    State = FileState.Message;
                    needUpdate = true;
                } else {
                    react = NameField.HandleKeystroke(keyInfo, out needUpdate);
                }
            } else if (State == FileState.Message) {
                State = FileState.View;
                needUpdate = true;
            }

            NeedUpdate = needUpdate;
            return react;
        }
        protected override void ShowMenu() {
            ShowMenuCommand("Esc", "Назад");
            ShowMenuCommand("F1", "Сохранить", State == FileState.Save);
            Console.WriteLine();
        }
    }
}
