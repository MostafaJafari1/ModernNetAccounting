using Accounting.Core.Domain.Accounts.Enums;
using Accounting.Core.Domain.Accounts.ValueObjects;
using BuildingBlocks.Domain.Primitives.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Domain.Accounts;

/// <summary>
/// Represents an Account within the Chart of Accounts 
/// (commonly referred to as "Coding" in Iranian accounting systems).
/// Supports a hierarchical/tree structure.
/// </summary>
public class Account : AuditableAggregateRoot
{

    #region Properties

    public AccountCode Code { get; private set; }

    public AccountName Name { get; private set; }

    public Guid? ParentId { get; private set; }

    public Account Parent { get; private set; }

    public AccountLevel Level { get; private set; }

    public AccountNature Nature { get; private set; }

    public bool IsActive { get; private set; }

    // Indicates if transactions can be directly posted to this account
    public bool IsPostable { get; private set; }
    #endregion

    #region Constructors
    protected Account() : base(Guid.Empty) { }

    private Account(Guid id, AccountCode code, AccountName name, Guid? parentId, AccountLevel level, AccountNature nature, bool isPostable)
        : base(id)
    {
        Code = code;
        Name = name;
        ParentId = parentId;
        Level = level;
        Nature = nature;
        IsPostable = isPostable;
        IsActive = true;
    }
    #endregion

    #region Methods

    public static Account Create(AccountCode code, AccountName name, Guid? parentId, AccountLevel level, AccountNature nature, bool isPostable)
    {
        return new Account(Guid.NewGuid(), code, name, parentId, level, nature, isPostable);
    }

    public void Deactivate()
    {
        this.IsActive = false;
    }

    public void UpdateDetails(AccountName name, AccountNature nature)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Nature = nature;
    } 

    #endregion
}



