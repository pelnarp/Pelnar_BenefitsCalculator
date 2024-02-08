using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Services;
using Api.Services.DeductionRules;
using Xunit;

namespace ApiTests.UnitTests.Services;

public class DeductionProcessorTests
{
    [Fact]
    public async Task ForAgeBeloeLimit_ShouldNOTDeduct()
    {
        //Arrange
        var rules = new List<IDeductionRule>
        {
            // TODO: add mocks implementations, for example Moq
        };
        var target = new DeductionProcessor(rules);
        var employee = new Employee()
        {
            Salary = 100000
        };

        // Act
        var result = target.GetSalaryAfterDeductions(employee);

        // TODO: assert all rules were called once for each of them using mock  
        Assert.True(true);
    }
}
