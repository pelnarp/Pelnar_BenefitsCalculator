using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Services.DeductionRules;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ApiTests.UnitTests.Services.DeductionRules;

public class MonthlyBaseCostsDeductionRuleTests
{
    [Fact]
    public async Task ShouldDeduct12xBaseValueFromConfig()
    {
        //Arrange
        var monthlyValue = 2000;
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
            {"MonthlyBaseCosts", monthlyValue.ToString()},
        }).Build();
        var employee = new Employee();
        var target = new MonthlyBaseCostsDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 12 * monthlyValue);
    }

    // TODO: more tests...
}



