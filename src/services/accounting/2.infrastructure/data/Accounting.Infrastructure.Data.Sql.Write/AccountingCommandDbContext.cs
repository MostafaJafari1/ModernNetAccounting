using Accounting.Core.Domain.Accounts;
using Accounting.Infrastructure.Data.Sql.Write.Accounts;
using BuildingBlocks.Infrastructure.Data.Sql.Write;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Data.Sql.Write;

public class AccountingCommandDbContext : BaseCommandDbContext
{
    public AccountingCommandDbContext(DbContextOptions<AccountingCommandDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountConfiguration).Assembly);
    }
}