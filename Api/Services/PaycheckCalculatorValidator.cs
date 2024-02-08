using Api.Models;

namespace Api.Services
{
    /// <summary>
    /// Assures the validation rules for the operation is should cover. This covers calculations of paychecks by PaycheckCalculator
    /// In cases when validation fails which is considered edge case of the business logic, exeption is thrown
    /// </summary>
    public class PaycheckCalculatorValidator : IPaycheckCalculatorValidator
    {
        public void ValidateForCalculation(Employee employee)
        {
            // TODO: all these could be broken down into independent calidation rules. A framework such as FluentValidation could by used for this functionality at larger scale

            // Initial Checks
            if (employee == null)
                throw new ArgumentException("Employee must not be null", nameof(employee));

            if (employee.Salary < 0)
                throw new ArgumentException($"Invalid salary value: {employee.Salary}", nameof(employee.Salary));

            // Data requirement checks
            if (employee.Dependents.Count(x => x.Relationship != Relationship.None && x.Relationship != Relationship.Child) > 1)
                throw new ApplicationException($"Employee has more than one spouse or domestic partner. Employee ID: {employee.Id}");
        }
    }

}
