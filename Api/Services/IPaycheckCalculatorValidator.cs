using Api.Models;

namespace Api.Services
{
    /// <summary>
    /// Assures the validation rules for the operation is should cover. This covers calculations of paychecks by PaycheckCalculator
    /// </summary>
    public interface IPaycheckCalculatorValidator
    {
        void ValidateForCalculation(Employee employee);
    }

}
