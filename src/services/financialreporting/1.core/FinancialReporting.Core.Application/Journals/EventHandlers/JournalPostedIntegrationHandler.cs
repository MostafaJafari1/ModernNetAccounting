using BuildingBlocks.Application.CQRS.Events;
using FinancialReporting.Core.Contracts.Journals.IntegrationEvents;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Wolverine;

namespace FinancialReporting.Core.Application.Journals.EventHandlers;
public class JournalPostedIntegrationHandler(
    ILogger<JournalPostedIntegrationHandler> logger,
    IMessageBus _bus)
    : BaseIntegrationEventHandler<JournalEntryPostedIntegrationEvent>(logger)
{
    protected override async Task HandleAsync(
        JournalEntryPostedIntegrationEvent domainEvent,
        CancellationToken cancellationToken)
    {
        
    }
}