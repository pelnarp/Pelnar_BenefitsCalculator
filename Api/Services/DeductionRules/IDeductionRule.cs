using Api.Models;

namespace Api.Services.DeductionRules
{
    public interface IDeductionRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">Onoging paycheck calculation</param>
        /// <param name="employee">Employee for whome the rules are applied</param>
        /// <returns></returns>
        public decimal GetDeduction(Employee employee);
    }
}
