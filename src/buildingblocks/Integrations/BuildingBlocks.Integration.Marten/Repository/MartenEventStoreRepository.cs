using BuildingBlocks.Integration.Marten.Events;
using JasperFx.Events;
using Marten;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Integration.Marten.Repository;

public class MartenEventStoreRepository<TAggregate> :
    IMartenEventStoreRepository<TAggregate>
    where TAggregate : class
{
    private readonly IDocumentSession _session;

    public MartenEventStoreRepository(IDocumentSession session)
    {
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }

    //Optimistic Concurrency Support
    public async Task AppendEventsAsync(
        Guid streamId,
        IEnumerable<object> events,
        long? expectedVersion = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(events);

        var eventList = events.ToList();
        if (eventList.Count == 0)
            return;

        if (expectedVersion.HasValue)
        {
            _session.Events.Append(streamId, expectedVersion.Value, eventList.ToArray());
        }
        else
        {
            _session.Events.Append(streamId, eventList.ToArray());
        }

        await _session.SaveChangesAsync(cancellationToken);
    }

    public async Task<TAggregate?> GetByIdAsync(
        Guid streamId,
        long? version = null,
        CancellationToken cancellationToken = default)
    {
        if (version.HasValue)
        {
            return await _session.Events.AggregateStreamAsync<TAggregate>(
                streamId,
                version: version.Value,
                token: cancellationToken);
        }

        return await _session.Events.AggregateStreamAsync<TAggregate>(
            streamId,
            token: cancellationToken);
    }

    public async Task<StreamState?> GetStreamStateAsync(
        Guid streamId,
        CancellationToken cancellationToken = default)
    {
        return await _session.Events.FetchStreamStateAsync(streamId, cancellationToken);
    }

    public async Task ArchiveAsync(
        Guid streamId,
        CancellationToken cancellationToken = default)
    {
        var state = await _session.Events.FetchStreamStateAsync(streamId, cancellationToken);

        if (state is null)
            throw new InvalidOperationException($"Stream {streamId} پیدا نشد.");

        _session.Events.Append(streamId, state.Version, new StreamArchivedEvent(streamId, DateTimeOffset.UtcNow));
        await _session.SaveChangesAsync(cancellationToken);
    }
}