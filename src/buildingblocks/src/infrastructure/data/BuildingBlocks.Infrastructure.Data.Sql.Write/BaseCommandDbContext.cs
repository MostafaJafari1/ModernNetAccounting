using BuildingBlocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace BuildingBlocks.Infrastructure.Data.Sql.Write;

public abstract class BaseCommandDbContext : DbContext
{
    protected BaseCommandDbContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    public override int SaveChanges()
    {
        ApplyAuditInfo();
        var result = base.SaveChanges();
        return result;
    }

    private void ApplyAuditInfo()
    {
        var entries = ChangeTracker.Entries<IAuditableEntity>();

        var currentUserId = "System";
        var currentTimeUtc = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = currentTimeUtc;
                entry.Entity.CreatedBy = currentUserId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedAt = currentTimeUtc;
                entry.Entity.LastModifiedBy = currentUserId;

                entry.Property(x => x.CreatedAt).IsModified = false;
                entry.Property(x => x.CreatedBy).IsModified = false;
            }
        }
    }
}