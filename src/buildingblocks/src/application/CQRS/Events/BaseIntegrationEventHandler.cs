using BuildingBlocks.Contracts.Application.CQRS.Event;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Application.CQRS.Events;
public abstract class BaseIntegrationEventHandler<TEvent>
    : IIntegrationEventHandler<TEvent>
    where TEvent : IIntegrationEvent
    {
        protected readonly ILogger Logger;

        protected BaseIntegrationEventHandler(ILogger logger)
        {
            Logger = logger;
        }

        public async Task Handle(
            TEvent integrationEvent,
            CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation(
                    "Started processing Integration Event: {EventType} - Id: {EventId}",
                    typeof(TEvent).Name,
                    integrationEvent.Id);

                await HandleAsync(integrationEvent, cancellationToken);

                Logger.LogInformation(
                    "Integration Event processed successfully: {EventType} - Id: {EventId}",
                    typeof(TEvent).Name,
                    integrationEvent.Id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,
                    "Error occurred while processing Integration Event: {EventType} - Id: {EventId}",
                    typeof(TEvent).Name,
                    integrationEvent.Id);

                throw;
            }
        }

        protected abstract Task HandleAsync(
            TEvent integrationEvent,
            CancellationToken cancellationToken);
    }