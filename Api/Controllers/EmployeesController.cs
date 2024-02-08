using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : EntityControllerBase<Employee, GetEmployeeDto>
{
    public EmployeesController(IRepository<Employee> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    [SwaggerOperation(Summary = "Get employee by id")] // We override only to specify the Swagger description, probably can be done in a better way
    [HttpGet("{id}")]
    override public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        return await base.Get(id);
    }

    [SwaggerOperation(Summary = "Get all employees")] // We override only to specify the Swagger description, probably can be done in a better way
    [HttpGet("")]
    override public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        return await base.GetAll();
    }
}
