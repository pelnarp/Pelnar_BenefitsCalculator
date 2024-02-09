using Api.Models;

namespace Api.Services.DeductionRules
{
    /// <summary>
    /// dependents that are over 50 years old will incur an additional $200 per month
    /// </summary>
    public class ElderyDependentDeductionRule : IDeductionRule
    {
        private readonly IConfiguration configuration;

        public ElderyDependentDeductionRule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public decimal GetDeduction(Employee employee)
        {
            var ageLimit = int.Parse(configuration["ElderyAgeLimit"]); // Let's keep this configurable from the appsettings
            var ageLimitDeduction = int.Parse(configuration["ElderyAgeLimitDeduction"]); // Let's keep this configurable from the appsettings

            var elderyDependentsCount = employee.Dependents.Count(x => x.DateOfBirth <= DateTime.UtcNow.AddYears(-ageLimit));

            return elderyDependentsCount * ageLimitDeduction * 12; // $200 per eldery dependant per month
        }
    }
}
