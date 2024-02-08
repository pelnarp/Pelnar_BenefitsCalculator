using Api.Models;

namespace Api.Services
{
    public interface IDeductionProcessor
    {
        decimal GetSalaryAfterDeductions(Employee employee);
    }
}