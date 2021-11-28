using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Task_7._2._2 {
    enum DirectoryState {
        InputPath,
        View,
        CreateFolder,
        Delete,
        Rename,
        Message
    }

    class DirectoryView : ConsoleItem {
        DirectoryInfo DirectoryInfo { get; set; }
        SelectableConsoleTable SelectableTable { get; }
        WriteField NameField { get; } = new WriteField();
        DirectoryState State { get; set; }
        string Message { get; set; } = "";

        public DirectoryView(ConsoleItem prev, string path = null) : base(prev) {
            if (path == null) {
                State = DirectoryState.InputPath;
                NameField.Text = Environment.CurrentDirectory;
            } else {
                if (Directory.Exists(path)) {
                    DirectoryInfo = new DirectoryInfo(path);
                    State = DirectoryState.View;
                } else {
                    throw new ArgumentException("Неправильный путь.");
                }
            }
            SelectableTable = new SelectableConsoleTable(
                new int[] { Console.WindowWidth / 2, Console.WindowWidth / 4 - 1, Console.WindowWidth / 4 - 2 },
                new Alignment[] { Alignment.Left, Alignment.Right, Alignment.Center },
                new string[] { "Название", "Размер", "Последнее редактирование" });
        }


        public override void Show() {
            base.Show();

            if (NeedUpdate) {
                if (State == DirectoryState.InputPath) {
                    Console.CursorVisible = true;
                    Console.WriteLine("Введите путь к папке:");
                    NameField.Show();
                    Console.CursorTop = NameField.ConsoleCursor.Top;
                    Console.CursorLeft = NameField.ConsoleCursor.Left;
                } else if (State == DirectoryState.View) {
                    Console.CursorVisible = false;
                    FileSystemInfo[] infos = DirectoryInfo.GetFileSystemInfos();
                    int position = SelectableTable.Position;
                    UpdateTable(infos);
                    SelectableTable.Position = position;
                    SelectableTable.Show();
                } else if (State == DirectoryState.CreateFolder) {
                    Console.CursorVisible = true;
                    Console.WriteLine("Создать папку:");
                    NameField.Show();
                    Console.CursorTop = NameField.ConsoleCursor.Top;
                    Console.CursorLeft = NameField.ConsoleCursor.Left;
                } else if (State == DirectoryState.Delete) {
                    Console.CursorVisible = true;
                    FileSystemInfo info = DirectoryInfo.GetFileSystemInfos()[SelectableTable.Position];
                    Console.WriteLine("Вы действительно хотите удалить {0} {1}", info is DirectoryInfo ? "папку" : "файл", info.Name);
                    Console.Write("д/н?");
                } else if (State == DirectoryState.Rename) {
                    Console.CursorVisible = true;
                    FileSystemInfo info = DirectoryInfo.GetFileSystemInfos()[SelectableTable.Position];
                    Console.WriteLine("Переименовать {0} {1} в:", info is DirectoryInfo ? "папку" : "файл", info.Name);
                    NameField.Show();
                    Console.CursorTop = NameField.ConsoleCursor.Top;
                    Console.CursorLeft = NameField.ConsoleCursor.Left;
                } else if (State == DirectoryState.Message) {
                    Console.CursorVisible = true;
                    Console.Write($"{Message} Нажмите чтобы продолжить...");
                }

                NeedUpdate = false;
            }
        }

        protected override void ShowMenu() {
            ShowMenuCommand("Esc", Prev == null ? "Выйти" : "Назад");
            ShowMenuCommand("Enter", "Перейти");
            ShowMenuCommand("Delete", "Удалить");
            ShowMenuCommand("F1", "Переименовать", State == DirectoryState.Rename);
            ShowMenuCommand("F2", "Новая папка", State == DirectoryState.CreateFolder);
            ShowMenuCommand("F3", "Новый файл", false);
            Console.WriteLine();
        }

        void UpdateTable(FileSystemInfo[] infos) {
            SelectableTable.ClearRows();
            foreach (var info in infos) {
                if (info is FileInfo fileInfo) {
                    SelectableTable.AddRow(fileInfo.Name, $"{fileInfo.Length / 1024} КБ", fileInfo.LastWriteTime.ToString("dd.MM.yyyy"));
                } else if (info is DirectoryInfo directoryInfo) {
                    SelectableTable.AddRow($"\\{directoryInfo.Name}", "-", "-");
                }
            }
        }

        public override bool HandleKeyStroke(ConsoleKeyInfo keyInfo, out bool needUpdate) {
            needUpdate = false;
            bool react = true;

            if (State == DirectoryState.InputPath) {

                if (keyInfo.Key == ConsoleKey.Escape) {
                    Next = Prev;
                } else if (keyInfo.Key == ConsoleKey.Enter) {
                    string path = NameField.Text;
                    if (Directory.Exists(path)) {
                        DirectoryInfo = new DirectoryInfo(path);
                        State = DirectoryState.View;
                        needUpdate = true;
                    } else {
                        State = DirectoryState.InputPath;
                        NameField.Text = Environment.CurrentDirectory;
                        needUpdate = true;
                    }
                } else {
                    react = NameField.HandleKeystroke(keyInfo, out needUpdate);
                }

            } else if (State == DirectoryState.View) {

                if (keyInfo.Key == ConsoleKey.Escape) {
                    Next = Prev;
                } else if (keyInfo.Key == ConsoleKey.F1) {
                    NameField.Text = DirectoryInfo.GetFileSystemInfos()[SelectableTable.Position].Name;
                    State = DirectoryState.Rename;
                    needUpdate = true;
                } else if (keyInfo.Key == ConsoleKey.F2) {
                    NameField.Text = "";
                    State = DirectoryState.CreateFolder;
                    needUpdate = true;
                } else if (keyInfo.Key == ConsoleKey.F3) {
                    Next = new FileView(this, DirectoryInfo.FullName);
                } else if (keyInfo.Key == ConsoleKey.Enter) {
                    FileSystemInfo info = DirectoryInfo.GetFileSystemInfos()[SelectableTable.Position];
                    if (info is DirectoryInfo directoryInfo) {
                        try {
                            Next = new DirectoryView(this, directoryInfo.FullName);
                        } catch (Exception) {
                            Next = this;
                            State = DirectoryState.Message;
                            Message = "Невозможно перейти к выбранной папке.";
                        }
                    } else if (info is FileInfo fileInfo && fileInfo.Length < 1024 * 1024 * 10) {
                        try {
                            Next = new FileView(this, fileInfo.DirectoryName, fileInfo.Name);
                        } catch (Exception) {
                            Next = this;
                            State = DirectoryState.Message;
                            Message = "Невозможно перейти к выбранному файлу.";
                        }
                    }
                } else if (keyInfo.Key == ConsoleKey.Delete) {
                    State = DirectoryState.Delete;
                    needUpdate = true;
                } else {
                    react = SelectableTable.HandleKeystroke(keyInfo, out needUpdate);
                }

            } else if (State == DirectoryState.CreateFolder) {

                if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.F2) {
                    State = DirectoryState.View;
                    needUpdate = true;
                } else if (keyInfo.Key == ConsoleKey.Enter && NameField.Text.Length > 0) {
                    try {
                        DirectoryInfo.CreateSubdirectory(NameField.Text);
                        Message = "Папка успешно создана.";
                    } catch (Exception) {
                        Message = "Невозможно создать папку.";

                    }
                    State = DirectoryState.Message;
                    needUpdate = true;
                } else {
                    react = NameField.HandleKeystroke(keyInfo, out needUpdate);
                }

            } else if (State == DirectoryState.Delete) {

                if (keyInfo.Key == ConsoleKey.Escape || keyInfo.KeyChar == 'н' || keyInfo.KeyChar == 'n') {
                    State = DirectoryState.View;
                    needUpdate = true;
                } else if (keyInfo.KeyChar == 'д' || keyInfo.KeyChar == 'y') {
                    FileSystemInfo info = DirectoryInfo.GetFileSystemInfos()[SelectableTable.Position];
                    if (info is DirectoryInfo directoryInfo) {
                        try {
                            directoryInfo.Delete(true);
                            Message = "Папка успешно удалена.";
                        } catch (Exception) {
                            Message = "Невозможно удалить папку.";
                        }
                        needUpdate = true;
                    } else if (info is FileInfo fileInfo) {
                        try {
                            fileInfo.Delete();
                            Message = "Файл успешно удален.";
                        } catch (Exception) {
                            Message = "Невозможно удалить файл.";
                        }
                        needUpdate = true;
                    }
                    State = DirectoryState.Message;
                }

            } else if (State == DirectoryState.Rename) {

                if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.F1) {
                    State = DirectoryState.View;
                    needUpdate = true;
                } else if (keyInfo.Key == ConsoleKey.Enter) {
                    FileSystemInfo info = DirectoryInfo.GetFileSystemInfos()[SelectableTable.Position];
                    if (info is DirectoryInfo directoryInfo) {
                        try {
                            directoryInfo.MoveTo(Path.Combine(DirectoryInfo.FullName, NameField.Text));
                            Message = "Папка успешно переименована.";
                        } catch (Exception) {
                            Message = "Невозможно переименовать папку.";
                        }
                        needUpdate = true;
                    } else if (info is FileInfo fileInfo) {
                        try {
                            string path = Path.Combine(DirectoryInfo.FullName, NameField.Text);
                            if (!Path.HasExtension(path)) {
                                path = Path.ChangeExtension(path, fileInfo.Extension);
                            }
                            fileInfo.MoveTo(Path.Combine(DirectoryInfo.FullName, NameField.Text));
                            Message = "Файл успешно переименован.";
                        } catch (Exception) {
                            Message = "Невозможно переименовать файл.";
                        }
                        needUpdate = true;
                    }
                    State = DirectoryState.Message;
                } else {
                    react = NameField.HandleKeystroke(keyInfo, out needUpdate);
                }

            } else if (State == DirectoryState.Message) {

                State = DirectoryState.View;
                needUpdate = true;

            }

            NeedUpdate = needUpdate;
            return react;
        }

        protected override void OnResizeConsoleWindow() {
            SelectableTable.ColumnsWidth[0] = Console.WindowWidth / 2;
            SelectableTable.ColumnsWidth[1] = Console.WindowWidth / 4 - 1;
            SelectableTable.ColumnsWidth[2] = Console.WindowWidth / 4 - 2;
        }
    }
}
