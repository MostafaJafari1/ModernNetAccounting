using Accounting.Core.Contracts.Accounts;
using Accounting.Infrastructure.Data.Sql.Write.Accounts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore; 
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Accounting.Infrastructure.Data.Sql.Write;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddAccountingInfrastructure(this IHostApplicationBuilder builder,string connectionString)
    {
        builder.Services.AddDbContext<AccountingCommandDbContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddScoped<DbContext>(provider =>
            provider.GetRequiredService<AccountingCommandDbContext>());

        builder.Services.AddScoped<IAccountCommandRepository, AccountCommandRepository>();

        return builder;
    }
}