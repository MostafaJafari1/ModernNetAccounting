using Accounting.Core.Domain.Accounts;
using Accounting.Infrastructure.Data.Sql.Write.Accounts.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Accounting.Infrastructure.Data.Sql.Write.Accounts;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts", "Accounting");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Code)
            .HasConversion<AccountCodeConverter>()
            .HasColumnName("Code")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(a => a.Code).IsUnique();

        builder.Property(a => a.Name)
            .HasConversion<AccountNameConverter>()
            .HasColumnName("Name")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(a => a.Level)
            .HasConversion<AccountLevelConverter>()
            .HasColumnName("Level")
            .IsRequired();

        builder.Property(a => a.Nature)
            .HasConversion<AccountNatureConverter>()
            .HasColumnName("Nature")
            .IsRequired();

        builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(a => a.IsPostable).IsRequired().HasDefaultValue(false);

        builder.HasOne(a => a.Parent)
            .WithMany()
            .HasForeignKey(a => a.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        //Audits
        builder.Property(a => a.CreatedAt).IsRequired();
        builder.Property(a => a.CreatedBy).HasMaxLength(100).IsRequired();
        builder.Property(a => a.LastModifiedAt);
        builder.Property(a => a.LastModifiedBy).HasMaxLength(100);
    }
}