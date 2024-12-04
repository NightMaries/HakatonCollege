using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SqlKata;
using SqlKata.Execution;
using SqlKata.Compilers;
using SqlKata.Extensions;
using Microsoft.Data.SqlClient;
using Hakaton.API.Domen.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Hakaton.API.Infrustructure.Data;

public class HakatonContext: DbContext
{
    DbSet<User> Users {get; set;}
    DbSet<Replacement> Replacements {get; set;}

    DbSet<Role> Roles {get; set;}

    DbSet<Student> Students {get; set;}
    
    DbSet<Subject> Subjects {get; set;}
    
    DbSet<Schedule> Schedules {get; set;}
    
    DbSet<Teacher> Teachers {get; set;}
    DbSet<StudyWeek> StudyWeeks {get; set;}
    DbSet<Group> Groups {get; set;}    
    DbSet<ScheduleForTeacher>ScheduleForTeachers {get; set;}
    DbSet<PushReplacementUser> PushReplacementUsers {get; set;}    
    private readonly IConfiguration _configuration;
    private readonly ILogger<HakatonContext> _logger;
    private string NpgsqlConnectionString => _configuration["ConnectionStrings:DefaultConnection"];

    public HakatonContext(DbContextOptions<HakatonContext> options,
                        IConfiguration configuration,ILogger<HakatonContext> logger): base(options)
    {
        
        _configuration = configuration;
        _logger = logger;
    }

    private NpgsqlConnection PostgresDbConnection => new(NpgsqlConnectionString);
    public QueryFactory PostgresQueryFactory => new(PostgresDbConnection , new PostgresCompiler(),30)

    #if DEBUG
    {
        Logger = compiled => {_logger.LogInformation("Query = {@Query}",compiled.ToString());}
    }
    #endif
    ;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(NpgsqlConnectionString);
    }

}