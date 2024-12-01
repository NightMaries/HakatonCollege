using System.Security.Cryptography;
using System.Text;
using Azure.Core.Pipeline;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Application.Repositories;
using Hakaton.API.Application.Services;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.API.Controllers;

[ApiController]
[Route("[controller]")]

public class StudentController : ControllerBase
{
    private readonly IStudentRepository _repository;
    HMACSHA256 hmacStudent = new HMACSHA256();

    public StudentController(IStudentRepository repository)
    {
        _repository = repository;
    }

    //[HttpPost]

}