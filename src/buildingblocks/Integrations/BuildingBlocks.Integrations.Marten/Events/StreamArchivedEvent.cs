using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Integrations.Marten.Events;

// رویداد domain برای آرشیو - بخشی از مدل رویدادی شما
public record StreamArchivedEvent(Guid StreamId, DateTimeOffset ArchivedAt);
