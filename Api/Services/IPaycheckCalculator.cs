using Api.Models;

namespace Api.Services
{
    public interface IPaycheckCalculator
    {
        decimal CalculateEmployeePaychecks(Employee employee);
    }
}