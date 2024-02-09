using Api.Models;

namespace Api.Services.DeductionRules
{
    public class ElderyDeductionRule : IDeductionRule
    {
        private readonly IConfiguration configuration;

        public ElderyDeductionRule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public decimal GetDeduction(Employee employee)
        {
            var ageLimit = int.Parse(configuration["ElderyAgeLimit"]); // Let's keep this configurable from the appsettings
            var ageLimitDeduction = int.Parse(configuration["ElderyAgeLimitDeduction"]); // Let's keep this configurable from the appsettings

            return (employee.DateOfBirth <= DateTime.UtcNow.AddYears(-ageLimit)) ? (ageLimitDeduction * 12) : 0; // if salary is above limit calculate 2%, otherwise 0
        }
    }
}
