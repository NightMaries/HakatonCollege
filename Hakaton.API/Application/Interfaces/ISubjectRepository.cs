using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

public interface ISubjectRepository
{
    public Task<int> CreateSubject(SubjectDto subjectDto);
    public Task<Subject> GetSubjectById(int id);
    public Task<IEnumerable<Subject>> GetSubjects();
    public Task<int> EditSubject (SubjectDto subjectDto, int id);
    public Task<bool> DeleteSubject(int id);
}