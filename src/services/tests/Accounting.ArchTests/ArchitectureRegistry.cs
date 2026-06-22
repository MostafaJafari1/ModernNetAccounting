using Architecture = ArchUnitNET.Domain.Architecture;
using ArchUnitNET.Loader;
using System.Reflection;

namespace Accounting.ArchTests;

public class ArchitectureRegistry
{
    #region ArchUnitNET Load Assemblies

    public static readonly Architecture Architecture = new ArchLoader()
        .LoadAssemblies(
            // Accounting assemblies
            Assembly.GetAssembly(typeof(Accounting.Core.Domain.AssemblyReference))!,
            Assembly.GetAssembly(typeof(Accounting.Core.Application.AssemblyReference))!,
            Assembly.GetAssembly(typeof(Accounting.Core.Contracts.AssemblyReference))!,
            Assembly.GetAssembly(typeof(Accounting.Core.RequestResponse.AssemblyReference))!,
            Assembly.GetAssembly(typeof(Accounting.Core.Resources.AssemblyReference))!,
            Assembly.GetAssembly(typeof(Accounting.Infrastructure.Data.EventSourcing.Write.AssemblyReference))!,
            Assembly.GetAssembly(typeof(Accounting.Endpoints.Api.AssemblyReference))!,
            // Building blocks assemblies
            Assembly.GetAssembly(typeof(BuildingBlocks.Domain.AssemblyReference))!,
            Assembly.GetAssembly(typeof(BuildingBlocks.Application.AssemblyReference))!,
            Assembly.GetAssembly(typeof(BuildingBlocks.Infrastructure.AssemblyReference))!,
            Assembly.GetAssembly(typeof(BuildingBlocks.API.AssemblyReference))!,
            Assembly.GetAssembly(typeof(BuildingBlocks.Common.AssemblyReference))!,
            Assembly.GetAssembly(typeof(BuildingBlocks.Contracts.AssemblyReference))!,
            Assembly.GetAssembly(typeof(BuildingBlocks.Integrations.Marten.AssemblyReference))!,
            Assembly.GetAssembly(typeof(BuildingBlocks.Integrations.Wolverine.AssemblyReference))!

        )
        .Build();
    #endregion

    #region Assembly Helpers

    // Accounting assemblies

    public static Assembly DomainAssembly =>
        Assembly.GetAssembly(typeof(Accounting.Core.Domain.AssemblyReference))!;

    public static Assembly ApplicationAssembly =>
        Assembly.GetAssembly(typeof(Accounting.Core.Application.AssemblyReference))!;

    public static Assembly ContractsAssembly =>
        Assembly.GetAssembly(typeof(Accounting.Core.Contracts.AssemblyReference))!;

    public static Assembly RequestResponseAssembly =>
        Assembly.GetAssembly(typeof(Accounting.Core.RequestResponse.AssemblyReference))!;

    public static Assembly ResourcesAssembly =>
        Assembly.GetAssembly(typeof(Accounting.Core.Resources.AssemblyReference))!;

    public static Assembly InfrastructureAssembly =>
        Assembly.GetAssembly(typeof(Accounting.Infrastructure.Data.EventSourcing.Write.AssemblyReference))!;

    public static Assembly EndpointsAssembly =>
        Assembly.GetAssembly(typeof(Accounting.Endpoints.Api.AssemblyReference))!;


    // Building block assemblies
    public static Assembly BuildingBlocksDomainAssembly =>
        Assembly.GetAssembly(typeof(BuildingBlocks.Domain.AssemblyReference))!;

    public static Assembly BuildingBlocksApplicationAssembly =>
        Assembly.GetAssembly(typeof(BuildingBlocks.Application.AssemblyReference))!;

    public static Assembly BuildingBlocksInfrastructureAssembly =>
        Assembly.GetAssembly(typeof(BuildingBlocks.Infrastructure.AssemblyReference))!;

    public static Assembly BuildingBlocksApiAssembly =>
        Assembly.GetAssembly(typeof(BuildingBlocks.API.AssemblyReference))!;

    public static Assembly BuildingBlocksCommonAssembly =>
        Assembly.GetAssembly(typeof(BuildingBlocks.Common.AssemblyReference))!;

    public static Assembly BuildingBlocksContractsAssembly =>
        Assembly.GetAssembly(typeof(BuildingBlocks.Contracts.AssemblyReference))!;

    public static Assembly BuildingBlocksMartenAssembly =>
        Assembly.GetAssembly(typeof(BuildingBlocks.Integrations.Marten.AssemblyReference))!;

    public static Assembly BuildingBlocksWolverineAssembly =>
        Assembly.GetAssembly(typeof(BuildingBlocks.Integrations.Wolverine.AssemblyReference))!;

    #endregion
}
