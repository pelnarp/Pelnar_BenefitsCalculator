using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaychecksController : ControllerBase
{
    private readonly IRepository<Employee> employeeRepository;
    private readonly IPaycheckCalculator paycheckCalculator;

    public PaychecksController(IRepository<Employee> employeeRepository, IPaycheckCalculator paycheckCalculator)
    {
        this.employeeRepository = employeeRepository;
        this.paycheckCalculator = paycheckCalculator;
    }

    [SwaggerOperation(Summary = "Get employee's single paycheck amount after deductions by Employee id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> Get(int employeeId)
    {
        var employee = await employeeRepository.Get(employeeId);

        if (employee == null)
        {
            return NotFound(ApiResponse<GetPaycheckDto>.NotFoundResponse());
        }

        var calculatedPaycheck = paycheckCalculator.CalculateEmployeePaychecks(employee);

        var data = new GetPaycheckDto
        {
            EmployeeId = employee.Id,
            PaycheckAmountNumeric = calculatedPaycheck,
            PaycheckAmount = calculatedPaycheck.ToString("C")
        };

        return ApiResponse<GetPaycheckDto>.SuccessResponse(data);
    }
}
