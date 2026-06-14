using Marten;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using static Marten.MartenServiceCollectionExtensions;

namespace BuildingBlocks.Integration.Marten.Extensions;

public static class MartenBuilderExtensions
{
    public static MartenConfigurationExpression AddMartenDefaults(
        this IServiceCollection services,
        string connectionString,
        Action<StoreOptions>? configure = null,
        Action<MartenDefaults>? defaults = null)
    {
        // مقادیر پیش‌فرض
        var martenDefaults = new MartenDefaults();
        defaults?.Invoke(martenDefaults);

        return services.AddMarten(options =>
        {
            options.Connection(connectionString);
            options.AutoCreateSchemaObjects = martenDefaults.AutoCreate;
            options.Events.StreamIdentity = martenDefaults.StreamIdentity;

            configure?.Invoke(options);
        });
    }
}
