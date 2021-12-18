using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Task_1._2._1.Services {
    public interface IFileService {
        Task<(string, Exception)> GetAsync();
        Task<(string, Exception)> GetAsync(int row);
        Task<(string, Exception)> GetAsync(int startRow, int endRow);

        Task<(bool, Exception)> PutAsync(string text, bool force);
        Task<(bool, Exception)> PutAsync(string text, int row);

        Task<(bool, Exception)> DeleteAsync(int row);
    }

    public class FileService : IFileService {
        public string FilePath { get; }

        public FileService(IConfiguration configuration) {
            FilePath = Path.Combine(Directory.GetCurrentDirectory(), configuration["FileName"] ?? "text.txt");
            if (!File.Exists(FilePath)) {
                File.Create(FilePath).Close();
            }
        }

        public async Task<(string, Exception)> GetAsync() {
            try {
                return (await File.ReadAllTextAsync(FilePath), null);
            } catch (Exception e) {
                return (null, e);
            }
        }

        public async Task<(string, Exception)> GetAsync(int row) {
            if (row < 1) {
                return (null, new ArgumentOutOfRangeException(null, "В файле нет строки с таким номером."));
            }

            try {
                using var stream = File.OpenText(FilePath);
                for (int i = 0; i < row - 1 && !stream.EndOfStream; i++) {
                    await stream.ReadLineAsync();
                }

                if (stream.EndOfStream) {
                    return (null, new ArgumentOutOfRangeException(null, "В файле нет строки с таким номером."));
                } else {
                    return (await stream.ReadLineAsync(), null);
                }
            } catch (Exception e) {
                return (null, e);
            }
        }

        public async Task<(string, Exception)> GetAsync(int startRow, int endRow) {
            if (startRow < 1 || endRow <= startRow) {
                return (null, new ArgumentOutOfRangeException(null, "В файле нет строк с такими номерами."));
            }

            try {
                using var stream = File.OpenText(FilePath);
                for (int i = 0; i < startRow - 1 && !stream.EndOfStream; i++) {
                    await stream.ReadLineAsync();
                }

                if (stream.EndOfStream) {
                    return (null, new ArgumentOutOfRangeException(null, "В файле нет строк с такими номерами."));
                }

                var sb = new StringBuilder();

                int j;
                for (j = startRow; j < endRow && !stream.EndOfStream; j++) {
                    sb.Append(await stream.ReadLineAsync());
                    sb.Append("\n");
                }

                if (j != endRow) {
                    return (null, new ArgumentOutOfRangeException(null, "В файле нет строк с такими номерами."));
                } else {
                    return (sb.ToString(), null);
                }
            } catch (Exception e) {
                return (null, e);
            }
        }

        public async Task<(bool, Exception)> PutAsync(string text, bool force) {
            try {
                if (!force) {
                    int lineIndex = await IndexOfLine(text);
                    if (lineIndex != -1) {
                        return (false, new ArgumentException($"Данный текст уже сохранен в строке {lineIndex + 1}."));
                    }
                }

                text += "\r\n";
                await File.AppendAllTextAsync(FilePath, text);
                return (true, null);
            } catch (Exception e) {
                return (false, e);
            }
        }

        public async Task<int> IndexOfLine(string text) {
            return (await File.ReadAllLinesAsync(FilePath)).ToList().FindIndex(line => line == text);
        }

        public async Task<(bool, Exception)> PutAsync(string text, int row) {
            if (row < 1) {
                return (false, new ArgumentOutOfRangeException(null, "В файле нет строки с таким номером."));
            }

            try {
                var lines = await File.ReadAllLinesAsync(FilePath);

                if (lines.Length < row) {
                    return (false, new ArgumentOutOfRangeException(null, "В файле нет строки с таким номером."));
                }

                if (lines[row - 1] != text) {
                    lines[row - 1] = text;
                    await File.WriteAllLinesAsync(FilePath, lines);
                }

                return (true, null);
            } catch (Exception e) {
                return (false, e);
            }
        }

        public async Task<(bool, Exception)> DeleteAsync(int row) {
            if (row < 1) {
                return (false, new ArgumentOutOfRangeException(null, "В файле нет строки с таким номером."));
            }

            try {
                var lines = await File.ReadAllLinesAsync(FilePath);

                if (lines.Length < row) {
                    return (false, new ArgumentOutOfRangeException(null, "В файле нет строки с таким номером."));
                }

                await File.WriteAllLinesAsync(FilePath, lines.Select((line, index) => (line, index)).Where(item => item.index != row - 1).Select(item => item.line));

                return (true, null);
            } catch (Exception e) {
                return (false, e);
            }
        }
    }
}
