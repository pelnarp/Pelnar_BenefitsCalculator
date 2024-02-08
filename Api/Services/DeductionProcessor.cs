using Api.Models;
using Api.Services.DeductionRules;

namespace Api.Services
{
    public class DeductionProcessor : IDeductionProcessor
    {
        private readonly IEnumerable<IDeductionRule> rules;

        public DeductionProcessor(IEnumerable<IDeductionRule> rules)
        {
            this.rules = rules;
        }

        public decimal GetSalaryAfterDeductions(Employee employee)
        {
            var total = employee.Salary;
            foreach (var rule in rules)
            {
                total -= rule.GetDeduction(employee);
            }
            return total;
        }
    }
}
