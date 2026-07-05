using Accounting.Core.Contracts.Accounts;
using Accounting.Core.Domain.Accounts.Services;
using Accounting.Infrastructure.Data.Sql.Write.Accounts;
using Accounting.Infrastructure.Data.Sql.Write.Services;
using BuildingBlocks.Infrastructure.Data.Sql.Write;
using BuildingBlocks.Infrastructure.Data.Write;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Accounting.Infrastructure.Data.Sql.Write;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddAccountingInfrastructure(this IHostApplicationBuilder builder,string connectionString)
    {
        builder.Services.AddDbContext<AccountingCommandDbContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddScoped<DbContext>(provider =>
            provider.GetRequiredService<AccountingCommandDbContext>());

        builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork<AccountingCommandDbContext>>();

        builder.Services.AddScoped<IAccountUniquenessChecker, AccountUniquenessChecker>();

        builder.Services.AddScoped<IAccountCommandRepository, AccountCommandRepository>();

        return builder;
    }
}