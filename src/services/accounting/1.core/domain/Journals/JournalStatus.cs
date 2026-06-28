using BuildingBlocks.Domain.Primitives;
using System;

namespace Accounting.Core.Domain.Journals;

public sealed class JournalStatus : Enumeration<JournalStatus>
{
    /// <summary>
    /// The journal entry is being created or modified by the accountant. It has no financial impact and can be freely edited.
    /// </summary>
    public static readonly JournalStatus Draft = new(1, nameof(Draft));

    /// <summary>
    /// The journal entry has been submitted for review and is waiting for the financial manager's approval. It is locked for editing during this phase.
    /// </summary>
    public static readonly JournalStatus PendingApproval = new(2, nameof(PendingApproval));

    /// <summary>
    /// The financial manager rejected the journal entry due to errors. It is sent back to the accountant for corrections.
    /// </summary>
    public static readonly JournalStatus Rejected = new(3, nameof(Rejected));

    /// <summary>
    /// The journal entry has been verified and approved by the manager, but is not yet finalized in the general ledger.
    /// </summary>
    public static readonly JournalStatus Approved = new(4, nameof(Approved));

    /// <summary>
    /// The journal entry is officially posted and locked in the general ledger.
    /// </summary>
    public static readonly JournalStatus Posted = new(5, nameof(Posted));

    private JournalStatus(int value, string name) : base(value, name)
    {
    }

    /// <summary>
    /// Determines if the journal entry can be modified while in this status.
    /// </summary>
    public bool IsEditable => this == Draft || this == Rejected;

    /// <summary>
    /// Determines if the journal entry is locked and awaiting managerial action.
    /// </summary>
    public bool IsAwaitingReview => this == PendingApproval;

    public bool CanBePosted => this == Approved;
}