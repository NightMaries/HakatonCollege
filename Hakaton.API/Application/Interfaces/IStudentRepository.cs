using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

public interface IStudentRepository
{
    public Task<IEnumerable<Student>> GetStudents();
    public Task EditStudent (StudentDto studentDto, int id);
    public Task<bool> DeleteStudent(int id);
    public Task FindStudentById(int id);
    Task<int> CreateStudent(StudentDto student);
}