using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/employees-old")]
public class EmployeesControllerOld : ControllerBase // TODO: this has been simplified by adding a generic controller base which uses generic repository
{
    private readonly IRepository<Employee> employeeRepository;
    private readonly IMapper mapper;

    public EmployeesControllerOld(IRepository<Employee> employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = await employeeRepository.Get(id);

        if (employee == null) 
        {
            return NotFound(ApiResponse<GetEmployeeDto>.NotFoundResponse());
        }

        // We could try/catch here, but I would let it fail and handle by a higher level aspect
        var data = mapper.Map<GetEmployeeDto>(employee); // better on a single line for better debuggingn when needed
        return ApiResponse<GetEmployeeDto>.SuccessResponse(data);

    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var employees = await employeeRepository.GetAll();

        // We could try/catch here, but I would let it fail and handle by a higher level aspect
        var data = mapper.Map<List<GetEmployeeDto>>(employees); // better on a single line for better debuggingn when needed
        return ApiResponse<List<GetEmployeeDto>>.SuccessResponse(data);
    }
}
