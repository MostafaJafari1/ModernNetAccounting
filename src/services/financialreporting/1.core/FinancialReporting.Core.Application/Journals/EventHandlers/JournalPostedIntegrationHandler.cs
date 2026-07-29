using BuildingBlocks.Application.Common;
using BuildingBlocks.Application.CQRS.Events;
using BuildingBlocks.Integrations.Wolverine;
using FinancialReporting.Core.Application.Mappings;
using FinancialReporting.Core.Contracts.Journals.IntegrationEvents;
using FinancialReporting.Core.RequestResponse.Journals.Commands;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace FinancialReporting.Core.Application.Journals.EventHandlers;
public class JournalPostedIntegrationHandler(
    ILogger<JournalPostedIntegrationHandler> logger,
    IMessageBus _bus)
    : BaseIntegrationEventHandler<JournalEntryPostedIntegrationEvent>(logger)
{
    protected override async Task HandleAsync(
        JournalEntryPostedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        await _bus.SendCommandAsync<Unit>(command: integrationEvent.MapToCreateCommand());
    }
}

