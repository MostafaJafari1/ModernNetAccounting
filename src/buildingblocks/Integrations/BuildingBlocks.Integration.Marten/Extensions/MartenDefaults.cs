using JasperFx;
using JasperFx.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Integration.Marten.Extensions;

public class MartenDefaults
{
    public AutoCreate AutoCreate { get; set; } = AutoCreate.All;
    public StreamIdentity StreamIdentity { get; set; } = StreamIdentity.AsGuid;
}
