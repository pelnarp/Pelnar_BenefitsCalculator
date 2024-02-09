using Api.Models;

namespace Api.Services.DeductionRules
{
    /// <summary>
    /// employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs
    /// </summary>
    public class HighIncomeDeductionRule : IDeductionRule
    {
        private readonly IConfiguration configuration;

        public HighIncomeDeductionRule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public decimal GetDeduction(Employee employee)
        {
            var limit = int.Parse(configuration["HighIncomeLimit"]); // Let's keep this configurable from the appsettings
            var percentage = decimal.Parse(configuration["HighIncomePercentage"]); // Let's keep this configurable from the appsettings
            return (employee.Salary >= limit) ? employee.Salary * percentage : 0; // if salary is above limit calculate 2%, otherwise 0
        }
    }
}
