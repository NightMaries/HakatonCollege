using System.Text;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Application.Repositories;
using Hakaton.API.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Hakaton.API.Extections;
public static class JwtServices
{
    public static IServiceCollection AddJwtServices(this IServiceCollection services, IConfiguration configuration){
        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Приложение", Version = "v2024" });
                            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                            {
                                Description = "Authorization using jwt token. Example: \"Bearer {token}\"",
                                Name = "Authorization",
                                In = ParameterLocation.Header,
                                Type = SecuritySchemeType.ApiKey
                            });
                            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                            {
                                {
                                    new OpenApiSecurityScheme
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                        }
                                    },
                                    new string[] { }
                                }
                            });
                        });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.SaveToken = true;
                            options.RequireHttpsMetadata = true;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = false,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = "https://localhost:5058", // Укажите ваш адрес
                                ValidAudience = "https://localhost:5058",
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]!)),
                            };
                        });
        services.AddAuthorization();





        services.AddScoped<IStudyWeekRepository, StudyWeekRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPushReplacementUserRepository, PushReplacementUserRepository>();
        services.AddScoped<IReplacementRepository, ReplacementRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IPushReplacementService, PushReplacementService>();
        services.AddScoped<IParsingScheduleForTeachersService, ParsingScheduleForTeachersService>();
        services.AddScoped<ICryptService, CryptService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITeacherStatisticsRepository, TeacherStatisticsRepository>();
        services.AddScoped<ILoginService,LoginService>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IUserRepository,UserRepository>();

        return services;
    }
}