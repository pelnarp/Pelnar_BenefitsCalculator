using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Services.DeductionRules;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ApiTests.UnitTests.Services.DeductionRules;

public class DependantDeductionRuleTests
{
    [Fact]
    public async Task Apply_For2ChildrenAnd1WifeSHouldDeduct4TimesFromConfig()
    {
        //Arrange
        var perDependant = 1000;
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> {
            {"PerDependantCosts", perDependant.ToString()},
        }).Build();
        var employee = new Employee
        {
            Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
        };


        var target = new DependantDeductionRule(configuration);

        // Act
        var result = target.GetDeduction(employee);

        Assert.True(result == 3 * perDependant);
    }

    // TODO: more tests...

}

