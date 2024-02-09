using Api.Models;

namespace Api.Services
{
    public class PaycheckCalculator : IPaycheckCalculator
    {
        private readonly IConfiguration configuration;
        private readonly IDeductionProcessor deductionProcessor;
        private readonly IPaycheckCalculatorValidator paycheckCalculatorValidator;

        public PaycheckCalculator(IConfiguration configuration, IDeductionProcessor deductionProcessor, IPaycheckCalculatorValidator paycheckCalculatorValidator)
        {
            this.configuration = configuration;
            this.deductionProcessor = deductionProcessor;
            this.paycheckCalculatorValidator = paycheckCalculatorValidator;
        }

        public decimal CalculateEmployeePaychecks(Employee employee)
        {
            // Make the business logic check for correct data
            paycheckCalculatorValidator.ValidateForCalculation(employee);

            // Porcess all deductions
            var afterDeductions = this.deductionProcessor.GetSalaryAfterDeductions(employee);

            // Devide by total number of paychecks per year
            // TODO: we might want to round the pieces and spread evenly the rounded values
            // This could also be a part of deduciton rules (if considered as transformation rules)
            var paycheckPerYear = int.Parse(configuration["PaychecksPerYear"]); // Let's keep this configurable from the appsettings

            return afterDeductions / paycheckPerYear; // 26 paychecks per year with deductions spread as evenly as possible on each paycheck
        }
    }
}
