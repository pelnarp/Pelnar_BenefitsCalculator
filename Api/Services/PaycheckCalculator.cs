using Api.Models;

namespace Api.Services
{
    public class PaycheckCalculator : IPaycheckCalculator
    {
        private readonly IConfiguration configuration;
        private readonly IDeductionProcessor deductionProcessor;

        public PaycheckCalculator(IConfiguration configuration, IDeductionProcessor deductionProcessor)
        {
            this.configuration = configuration;
            this.deductionProcessor = deductionProcessor;
        }

        public decimal CalculateEmployeePaychecks(Employee employee)
        {
            // Initial Checks
            if (employee == null)
                throw new ArgumentException("Employee must not be null", nameof(employee));

            if (employee.Salary < 0)
                throw new ArgumentException($"Invalid salary value: {employee.Salary}", nameof(employee.Salary));

            // Data requirement checks
            if (employee.Dependents.Count(x => x.Relationship != Relationship.None && x.Relationship != Relationship.Child) > 1)
                throw new ApplicationException($"Employee has more than one spouse or domestic partner. Employee ID: {employee.Id}");

            // Porcess all deductions
            var afterDeductions = this.deductionProcessor.GetSalaryAfterDeductions(employee);

            // Devide by total number of paychecks per year
            // TODO: we might want to round the pieces and spread evenly the rounded values
            // This could also be a part of deduciton rules (if considered as transformation rules)
            var paycheckPerYear = int.Parse(configuration["PaychecksPerYear"]); // Let's keep this configurable from the appsettings
            return afterDeductions / paycheckPerYear;
        }
    }
}
