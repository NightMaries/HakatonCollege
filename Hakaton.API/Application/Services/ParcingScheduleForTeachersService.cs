using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Office.Y2022.FeaturePropertyBag;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

public class ParcingScheduleForTeachersService : IParcingScheduleForTeachersService
{

    /*public string Parcing()
    {
        string path = "Resources\\ScheludeForTeachers.xlsx";
        
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using(ExcelPackage excel = new ExcelPackage(path)){
        ExcelWorksheet excelWorksheet = excel.Workbook.Worksheets[0];
        string strings = "";
        for (int i = 1; i <= excelWorksheet.Dimension.Rows; i++)
        {
            for (int j = 1; j <= excelWorksheet.Dimension.Columns; j++)
            {
                Console.Write(excelWorksheet.Cells[j, i].Value + " ");
                strings += excelWorksheet.Cells[j, i].Value + " ";
            }
            Console.WriteLine();
            strings += '\n';
        }
        return strings;
        }
                
    }*/
    public string Parcing()
    {
        string path = "Resources\\ScheludeForTeachers.xlsx";
        
        // Устанавливаем контекст лицензии для EPPlus
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Используем StringBuilder для эффективного создания строки
        var resultBuilder = new StringBuilder();

        try
        {
            // Открываем Excel-файл
            using (var excel = new ExcelPackage(new FileInfo(path)))
            {
                // Получаем первый рабочий лист
                var excelWorksheet = excel.Workbook.Worksheets[0];

                // Перебор по столбцам
                for (int col = 1; col <= excelWorksheet.Dimension.Columns; col++)
                {
                    for (int row = 1; row <= excelWorksheet.Dimension.Rows; row++)
                    {
                        var cellValue = excelWorksheet.Cells[row, col].Text; // Получаем значение ячейки
                        resultBuilder.Append(cellValue).Append(" "); // Добавляем значение ячейки в строку
                    }
                    resultBuilder.AppendLine(); // Перевод строки после каждого столбца
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при парсинге файла: {ex.Message}");
            throw;
        }

        return resultBuilder.ToString(); // Возвращаем результат
    }
}


