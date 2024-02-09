using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Services.DeductionRules;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ApiTests.UnitTests.Services.DeductionRules;

public class ElderyDeductionRuleTests
{
    [Fact]
    public async Task ForAgeAboveLimit_ShouldDeduct_12Times200()
    {
        //Arrange
        var ageLimit = 50;
        var ageLimitDeduction = 200;

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
            {"ElderyAgeLimit", ageLimit.ToString()},
            {"ElderyAgeLimitDeduction", ageLimitDeduction.ToString()}
        }).Build();
        var employee = new Employee()
        {
            DateOfBirth = DateTime.UtcNow.AddYears(-51)
        };
        var target = new ElderyDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == ageLimitDeduction * 12);
    }

    [Fact]
    public async Task ForAgeAboveLimitOneHourOlder_ShouldDeduct_12Times200()
    {
        //Arrange
        var ageLimit = 50;
        var ageLimitDeduction = 200;

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
            {"ElderyAgeLimit", ageLimit.ToString()},
            {"ElderyAgeLimitDeduction", ageLimitDeduction.ToString()}
        }).Build();
        var employee = new Employee()
        {
            DateOfBirth = DateTime.UtcNow.AddYears(-50).AddHours(-1)
        };
        var target = new ElderyDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == ageLimitDeduction * 12);
    }

    [Fact]
    public async Task ForAgeBelowLimitOneHourYounger_ShouldNOTDeduct()
    {
        //Arrange
        var ageLimit = 50;
        var ageLimitDeduction = 200;

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
            {"ElderyAgeLimit", ageLimit.ToString()},
            {"ElderyAgeLimitDeduction", ageLimitDeduction.ToString()}
        }).Build();
        var employee = new Employee()
        {
            DateOfBirth = DateTime.UtcNow.AddYears(-50).AddHours(1)
        };
        var target = new ElderyDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 0);
    }

    [Fact]
    public async Task ForAgeBeloeLimit_ShouldNOTDeduct()
    {
        //Arrange
        var ageLimit = 50;
        var ageLimitDeduction = 200;

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
            {"ElderyAgeLimit", ageLimit.ToString()},
            {"ElderyAgeLimitDeduction", ageLimitDeduction.ToString()}
        }).Build();
        var employee = new Employee()
        {
            DateOfBirth = DateTime.UtcNow.AddYears(-30)
        };
        var target = new ElderyDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 0);
    }
}
