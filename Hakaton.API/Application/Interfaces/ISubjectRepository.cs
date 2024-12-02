using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

public interface ISubjectRepository
{
    public Task CreateSubject(SubjectDto subjectDto);
    public Task<Subject> GetSubjectById(int id);
    public Task<IEnumerable<Subject>> GetSubjects();
    public Task EditSubject (SubjectDto subjectDto, int id);
    public Task<bool> DeleteSubject(int id);
    public Task FindSubjectById(int id);
}