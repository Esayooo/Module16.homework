using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module16.homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string logFilePath = "C:\\Users\\ADMIN\\log.txt";
            Console.WriteLine("Добро пожаловать в файловый менеджер!");

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Просмотр содержимого директории");
                Console.WriteLine("2. Создание файла/директории");
                Console.WriteLine("3. Удаление файла/директории");
                Console.WriteLine("4. Копирование файла/директории");
                Console.WriteLine("5. Перемещение файла/директории");
                Console.WriteLine("6. Чтение из файла");
                Console.WriteLine("7. Запись в файл");
                Console.WriteLine("8. Выйти из программы");

                Console.Write("Введите номер действия: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListDirectoryContents();
                        break;
                    case "2":
                        CreateFileOrDirectory();
                        break;
                    case "3":
                        DeleteFileOrDirectory();
                        break;
                    case "4":
                        CopyFileOrDirectory();
                        break;
                    case "5":
                        MoveFileOrDirectory();
                        break;
                    case "6":
                        ReadFromFile();
                        break;
                    case "7":
                        WriteToFile();
                        break;
                    case "8":
                        Console.WriteLine("Программа завершена.");
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, выберите снова.");
                        break;
                }
            }
        }

        static void ListDirectoryContents()
        {
            Console.Write("Введите путь к директории: ");
            string directoryPath = Console.ReadLine();

            try
            {
                string[] files = Directory.GetFiles(directoryPath);
                string[] directories = Directory.GetDirectories(directoryPath);

                Console.WriteLine("\nСодержимое директории:");
                Console.WriteLine("Файлы:");
                foreach (string file in files)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }

                Console.WriteLine("\nДиректории:");
                foreach (string directory in directories)
                {
                    Console.WriteLine(Path.GetFileName(directory));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CreateFileOrDirectory()
        {
            Console.Write("Введите путь для создания файла/директории: ");
            string path = Console.ReadLine();

            Console.Write("Выберите (1 - файл, 2 - директория): ");
            string choice = Console.ReadLine();
            try
            {
                if (choice == "1")
                {
                    File.Create(path).Close();
                    Console.WriteLine("Файл успешно создан.");
                }
                else if (choice == "2")
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("Директория успешно создана.");
                }
                else
                {
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите 1 или 2.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void DeleteFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для удаления: ");
            string path = Console.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine("Файл успешно удален.");
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Console.WriteLine("Директория успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для копирования: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь для копирования: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)), true);
                    Console.WriteLine("Файл успешно скопирован.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    CopyDirectory(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Директория успешно скопирована.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void MoveFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для перемещения: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь для перемещения: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Файл успешно перемещен.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Директория успешно перемещена.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ReadFromFile()
        {
            Console.Write("Введите путь к файлу для чтения: ");
            string filePath = Console.ReadLine();

            try
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine($"\nСодержимое файла:\n{content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void WriteToFile()
        {
            Console.Write("Введите путь к файлу для записи: ");
            string filePath = Console.ReadLine();

            Console.Write("Введите текст для записи в файл: ");
            string content = Console.ReadLine();

            try
            {
                File.WriteAllText(filePath, content);
                Console.WriteLine("Текст успешно записан в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyDirectory(string source, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            string[] files = Directory.GetFiles(source);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destination, fileName);
                File.Copy(file, destFile, true);
            }

            string[] subdirectories = Directory.GetDirectories(source);
            foreach (string subdirectory in subdirectories)
            {
                string dirName = Path.GetFileName(subdirectory);
                string destDir = Path.Combine(destination, dirName);
                CopyDirectory(subdirectory, destDir);
            }
        }
    }
}
