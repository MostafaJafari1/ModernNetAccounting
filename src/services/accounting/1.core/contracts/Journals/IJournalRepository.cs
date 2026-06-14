using Accounting.Core.Domain.Journals;
using BuildingBlocks.Integration.Marten.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Core.Contracts.Journals;

public interface IJournalRepository : IMartenEventStoreRepository<JournalEntry> { }
