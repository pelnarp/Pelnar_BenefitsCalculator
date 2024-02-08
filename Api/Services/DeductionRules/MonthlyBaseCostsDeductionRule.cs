using Api.Models;

namespace Api.Services.DeductionRules
{
    public class MonthlyBaseCostsDeductionRule : IDeductionRule
    {
        private readonly IConfiguration configuration;

        public MonthlyBaseCostsDeductionRule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public decimal GetDeduction(Employee employee)
        {
            var monthlyBaseCosts = int.Parse(configuration["MonthlyBaseCosts"]); // Let's keep this configurable from the appsettings
            return 12 * monthlyBaseCosts;
        }
    }
}
