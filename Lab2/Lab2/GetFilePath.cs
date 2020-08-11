using System;
using System.IO;

namespace Lab2
{
    static class GetFilePath
    {
        static bool ExtensionCheck(string filePath)
        {
            const int minPathLength = 4;
            const string newValueAsk = "Введите путь ещё раз: ";
            if (filePath.Length > minPathLength)
            {
                if (filePath.EndsWith(".txt"))
                {
                    return true;
                }
                else
                {
                    ColorPrint.ErrorPrint("Неверное расширение! ", newValueAsk);
                    return false;
                }
            }
            else
            {
                ColorPrint.ErrorPrint("Слишком короткий путь к файлу! ", newValueAsk);
                return false;
            }
        }

        static void ContentCheck(string path)
        {
            int rowCntr = 1;
            if (new FileInfo(path).Length == 0)
                throw new Exception("Файл пуст!");
            using (var streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    string cluster = streamReader.ReadLine();
                    try
                    {
                        new Cluster(cluster);
                    }
                    catch
                    {
                        throw new Exception($"Неверный формат кластера на строке {rowCntr}");
                    }
                    ++rowCntr;
                }
            }
        }

        public static string GetFilePathForRead()
        {
            string path;
            const string newValueAsk = "Введите путь ещё раз: ";
            Console.Write("Введите путь к файлу для чтения таблицы кластеров (допускаются только .txt файлы): ");
            while (true)
            {
                path = Console.ReadLine();
                if (ExtensionCheck(path))
                {
                    try
                    {
                        using (var streamReader = new StreamReader(path)) { }
                        ContentCheck(path);
                        break;
                    }
                    catch (FileNotFoundException)
                    {
                        ColorPrint.ErrorPrint("Файла по указанному пути не существует! ", newValueAsk);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        ColorPrint.ErrorPrint("Доступ к файлу запрещён! ", newValueAsk);
                    }
                    catch (NotSupportedException)
                    {
                        ColorPrint.ErrorPrint("Запрещённое имя файла! ", newValueAsk);
                    }
                    catch (Exception e)
                    {
                        ColorPrint.ErrorPrint(e.Message, " " + newValueAsk);
                    }
                }
            }
            return path;
        }
    }
}