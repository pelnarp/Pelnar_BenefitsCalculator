using Api.Models;

namespace Api.Services.DeductionRules
{
    /// <summary>
    /// each dependent represents an additional $600 cost per month (for benefits)
    /// </summary>
    public class DependantDeductionRule : IDeductionRule
    {
        private readonly IConfiguration configuration;

        public DependantDeductionRule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public decimal GetDeduction(Employee employee)
        {
            var perDependantCost = int.Parse(configuration["PerDependantCosts"]); // Let's keep this configurable from the appsettings
            return employee.Dependents.Count * perDependantCost * 12;
        }
    }
}
