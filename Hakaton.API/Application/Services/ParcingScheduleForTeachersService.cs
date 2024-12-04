using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Office.Y2022.FeaturePropertyBag;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace Hakaton.API.Application.Services;

public class ParsingScheduleForTeachersService : IParsingScheduleForTeachersService
{
    private const string FilePath = "Resources\\ScheludeForTeachers.xlsx";
    private const string OutputFilePath = "Resources\\txtFiles\\ScheduleForTeachers1.txt";


    // public string Parse()
    // {
    //     // Устанавливаем лицензию для EPPlus
    //     ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

    //     // Используем StringBuilder для накопления результата
    //     var resultBuilder = new StringBuilder();

    //     try
    //     {
    //         // Загружаем файл Excel
    //         using (var excelPackage = new ExcelPackage(new FileInfo(FilePath)))
    //         {
    //             // Получаем первый лист из книги
    //             var worksheet = excelPackage.Workbook.Worksheets[0];

    //             if (worksheet?.Dimension == null)
    //             {
    //                 throw new InvalidOperationException("Лист Excel пустой или отсутствует.");
    //             }

    //             // Перебираем все строки и столбцы в листе
    //             for (int row = 1; row <= worksheet.Dimension.Rows; row++)
    //             {
    //                 for (int col = 1; col <= worksheet.Dimension.Columns; col++)
    //                 {                        
    //                     var cellValue = worksheet.Cells[row, col].Text.Trim(); // Убираем лишние пробелы
    //                     resultBuilder.Append(cellValue).Append(" "); // Добавляем значение ячейки
    //                 }
    //                 resultBuilder.AppendLine(); // Переходим на новую строку
    //             }
    //             var outputDirectory = Path.GetDirectoryName(OutputFilePath);
    //             if (!Directory.Exists(outputDirectory))
    //             {
    //                 Directory.CreateDirectory(outputDirectory);
    //             }

    //             // Записываем результат в текстовый файл
    //             File.WriteAllText(OutputFilePath, resultBuilder.ToString());

    //             Console.WriteLine($"Результат успешно сохранен в файл: {OutputFilePath}");
    //         }
    //     }
    //     catch (FileNotFoundException)
    //     {
    //         Console.WriteLine("Файл с расписанием не найден. Проверьте путь к файлу.");
    //         throw;
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"Ошибка при парсинге файла: {ex.Message}");
    //         throw;
    //     }

    //     return resultBuilder.ToString();
    // }
     public string Parse()
    {
        // Устанавливаем лицензию для EPPlus
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Используем StringBuilder для накопления результата
        var resultBuilder = new StringBuilder();

        try
        {
            // Загружаем файл Excel
            using (var excelPackage = new ExcelPackage(new FileInfo(FilePath)))
            {
                // Получаем первый лист из книги
                var worksheet = excelPackage.Workbook.Worksheets[0];

                if (worksheet?.Dimension == null)
                {
                    throw new InvalidOperationException("Лист Excel пустой или отсутствует.");
                }

                // Перебираем все строки и столбцы в листе
                for (int row = 1; row <= worksheet.Dimension.Rows; row++)
                {
                    for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                    {                        
                        var cellValue = worksheet.Cells[row, col].Text.Trim(); // Убираем лишние пробелы

                        // Проверяем пустую ячейку
                        if (string.IsNullOrEmpty(cellValue))
                        {
                            cellValue = "ПАРЫ НЕТ"; // Заменяем на "ПАРЫ НЕТ"
                        }

                        resultBuilder.Append(cellValue).Append("|"); // Добавляем значение ячейки
                    }
                    resultBuilder.AppendLine(); // Переходим на новую строку
                }

                var outputDirectory = Path.GetDirectoryName(OutputFilePath);
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                // Записываем результат в текстовый файл
                File.WriteAllText(OutputFilePath, resultBuilder.ToString());

                Console.WriteLine($"Результат успешно сохранен в файл: {OutputFilePath}");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл с расписанием не найден. Проверьте путь к файлу.");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при парсинге файла: {ex.Message}");
            throw;
        }

        return resultBuilder.ToString();
    }
}



