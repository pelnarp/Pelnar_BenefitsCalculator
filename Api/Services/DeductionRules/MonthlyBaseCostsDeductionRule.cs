using Api.Models;

namespace Api.Services.DeductionRules
{
    /// <summary>
    /// employees have a base cost of $1,000 per month (for benefits)
    /// </summary>
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
