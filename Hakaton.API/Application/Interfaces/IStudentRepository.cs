using Hakaton.API.Domen.Entities;

public interface IStudentRepository
{
    public Task CreateStudent (Student student);
    public Task<IEnumerable<Student>> GetStudents();
    public Task EditStudent (Student student, int id);
    public Task<bool> DeleteStudent(int id);
    public Task FindStudentById(int id);
}