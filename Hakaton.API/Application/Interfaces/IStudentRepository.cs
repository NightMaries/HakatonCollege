using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

public interface IStudentRepository
{
    public Task<IEnumerable<StudentDto>> GetStudents();
    public Task<int> EditStudent (StudentDto studentDto, int id);
    public Task<bool> DeleteStudent(int id);
    Task<int> CreateStudent(StudentDto student);
    Task<StudentDto> GetStudentById(int id);
}