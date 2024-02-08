using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Services.DeductionRules;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ApiTests.UnitTests.Services.DeductionRules;

public class HighIncomeDeductionRuleTests
{
    [Fact]
    public async Task ForAboveLimit_From100_000ShouldDeduct_2000()
    {
        //Arrange
        var highIncomeLimit = 80000;
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
            {"HighIncomeLimit", highIncomeLimit.ToString()},
            {"HighIncomePercentage", "0.02"}
        }).Build();
        var employee = new Employee()
        {
            Salary = 100000
        };
        var target = new HighIncomeDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 2000);
    }

    [Fact]
    public async Task ForAboveLimit_From80_000ShouldDeduct_1600()
    {
        //Arrange
        var highIncomeLimit = 80000;
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
            {"HighIncomeLimit", highIncomeLimit.ToString()},
            {"HighIncomePercentage", "0.02"}
        }).Build();
        var employee = new Employee()
        {
            Salary = 80000
        };
        var target = new HighIncomeDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 1600);
    }

    [Fact]
    public async Task BelowLimit79999_ShouldBe0()
    {
        //Arrange
        var highIncomeLimit = 80000;
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
            {"HighIncomeLimit", highIncomeLimit.ToString()},
            {"HighIncomePercentage", "0.02"}
        }).Build();
        var employee = new Employee()
        {
            Salary = 79999
        };
        var target = new HighIncomeDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 0);
    }

    [Fact]
    public async Task BelowLimit_ShouldBe0()
    {
        //Arrange
        var highIncomeLimit = 80000;
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
            {"HighIncomeLimit", highIncomeLimit.ToString()},
            {"HighIncomePercentage", "0.02"}
        }).Build();
        var employee = new Employee()
        {
            Salary = 50000
        };
        var target = new HighIncomeDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 0);
    }

    // TODO: more tests...
}
