using Api.DAL;
using Api.Models;
using Api.Services;
using Api.Services.DeductionRules;

namespace Api.IoC
{
    public static class BuilderExtensions
    {
        /// <summary>
        /// Configures dependencies required by this project
        /// In real life this could be per project, per logical part of the application, plugin, etc.
        /// </summary>
        /// <param name="builder"></param>
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IRepository<Dependent>, DependentRepository>();

            builder.Services.AddSingleton<IPaycheckCalculator, PaycheckCalculator>();
            builder.Services.AddSingleton<IDeductionProcessor, DeductionProcessor>();
            builder.Services.AddSingleton<IPaycheckCalculatorValidator, PaycheckCalculatorValidator>();

            builder.Services.AddSingleton<IDeductionRule, MonthlyBaseCostsDeductionRule>();
            builder.Services.AddSingleton<IDeductionRule, DependantDeductionRule>();
            builder.Services.AddSingleton<IDeductionRule, HighIncomeDeductionRule>();
            builder.Services.AddSingleton<IDeductionRule, ElderyDeductionRule>();
        }
    }
}
