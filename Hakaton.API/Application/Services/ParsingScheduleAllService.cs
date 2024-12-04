using System.Text;
using System.Text.RegularExpressions;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Application.Repositories;
using Hakaton.API.Domen.Dto;
using OfficeOpenXml;
namespace Hakaton.API.Application.Services;
public class ParsingScheduleAllService: IParsingScheduleAllService
{
    private readonly IGroupRepository _groupRepository;
    public ParsingScheduleAllService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    private const string FilePath = "Resources\\ScheludeForTeachers.xlsx";
    private const string OutputFilePath = "..\\..\\Resources\\txtFiles\\ScheduleForTeachers.txt";
    
    public async Task<string> ParseAsync()
    {
        // Устанавливаем лицензию для EPPlus
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Используем StringBuilder для накопления результата
        var resultBuilder = new StringBuilder();
        List<Group> listGroup = new List<Group>();
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


                var groupsDto =await _groupRepository.GetGroups();
                List<GroupDto> groups = new List<GroupDto>();
                // Перебираем все строки и столбцы в листе
                for (int row = 1; row <= worksheet.Dimension.Rows; row++)
                {
                    string day = worksheet.Cells[row, 1].Text;       // День недели
                    string pairNumber = worksheet.Cells[row, 2].Text; 

                    for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                    {
                        if(row == 5)
                        {   
                            groups.Add(new GroupDto{Name = worksheet.Cells[row, col].Text.Trim(),
                            TeacherId = 10});
                        }
                        
                        var cellValue = worksheet.Cells[row, col].Text.Trim(); // Убираем лишние пробелы
                        resultBuilder.Append(cellValue).Append(" "); // Добавляем значение ячейки
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