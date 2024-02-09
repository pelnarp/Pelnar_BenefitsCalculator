using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Services.DeductionRules;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ApiTests.UnitTests.Services.DeductionRules;

public class ElderyDependentDeductionRuleTests
{
    [Fact]
    public async Task For2DependantsAgeAboveLimit_ShouldDeduct_Twice12Times200()
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
            Dependents = new List<Dependent>
            {
                new ()
                {
                    DateOfBirth = DateTime.UtcNow.AddYears(-51)
                },
                new ()
                {
                    DateOfBirth = DateTime.UtcNow.AddYears(-51)
                }
            }

        };
        var target = new ElderyDependentDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 2 * ageLimitDeduction * 12);
    }

    [Fact]
    public async Task ForOneDependantAgeAboveLimitOneHourOlder_ShouldDeduct_12Times200()
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
            Dependents = new List<Dependent>
            {
                new ()
                {
            DateOfBirth = DateTime.UtcNow.AddYears(-50).AddHours(-1)
                },
            }

        };

        var target = new ElderyDependentDeductionRule(configuration);

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
            Dependents = new List<Dependent>
            {
                new ()
                {
                    DateOfBirth = DateTime.UtcNow.AddYears(-50).AddHours(1)
                },
                new ()
                {
                    DateOfBirth = DateTime.UtcNow.AddYears(-50).AddHours(1)
                }
            }
        };
        var target = new ElderyDependentDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 0);
    }

    [Fact]
    public async Task ForEmployeeWithoutDependants_ShouldNOTDeduct()
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
        };
        var target = new ElderyDependentDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 0);
    }
}
