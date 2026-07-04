using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using System.Reflection;

namespace Accounting.ArchTests;

//⭕ Important Point
//ArchUnit works by analyzing the compiled IL(intermediate language) bytecode, not the.csproj file.
//So it only sees real code-level dependencies — a class that inherits from another,
//a method that calls another, a type used as a parameter, a field typed as something
//from the other assembly, etc.
public class LayerDependencyTests
{
    // =====================================================================
    // Domain Layer
    // allowed  : BuildingBlocks.Domain, BuildingBlocks.Common, Accounting.Contracts
    // forbidden: everything else
    // =====================================================================

    [Fact]
    public void Domain_Should_Not_DependOn_Application()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.DomainAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.ApplicationAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Domain_Should_Not_DependOn_Infrastructure()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.DomainAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.InfrastructureAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Domain_Should_Not_DependOn_Endpoints()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.DomainAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.EndpointsAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Domain_Should_Not_DependOn_BuildingBlocks_Application()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.DomainAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.BuildingBlocksApplicationAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Domain_Should_Not_DependOn_BuildingBlocks_API()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.DomainAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.BuildingBlocksApiAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Domain_Should_Not_DependOn_BuildingBlocks_Marten()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.DomainAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.BuildingBlocksMartenAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Domain_Should_Not_DependOn_BuildingBlocks_Wolverine()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.DomainAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.BuildingBlocksWolverineAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    // =====================================================================
    // Application Layer
    // =====================================================================

    [Fact]
    public void Application_Should_Not_DependOn_Infrastructure()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.ApplicationAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.InfrastructureAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Application_Should_Not_DependOn_Endpoints()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.ApplicationAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.EndpointsAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Application_Should_Not_DependOn_BuildingBlocks_API()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.ApplicationAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.BuildingBlocksApiAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    // =====================================================================
    // Infrastructure Layer
    // allowed  : Application, Domain, Contracts, all BuildingBlocks except API
    // forbidden: Endpoints
    // =====================================================================

    [Fact]
    public void Infrastructure_Should_Not_DependOn_Endpoints()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.InfrastructureAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.EndpointsAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Infrastructure_Should_Not_DependOn_BuildingBlocks_API()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.InfrastructureAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.BuildingBlocksApiAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    // =====================================================================
    // Contracts Layer
    // allowed  : BuildingBlocks.Domain, BuildingBlocks.Common, BuildingBlocks.Contracts
    // forbidden: Application, Infrastructure, Endpoints, Domain, BuildingBlocks.Infrastructure, BuildingBlocks.API
    // =====================================================================

    [Fact]
    public void Contracts_Should_Not_DependOn_Domain()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.ContractsAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.DomainAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Contracts_Should_Not_DependOn_Application()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.ContractsAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.ApplicationAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Contracts_Should_Not_DependOn_Infrastructure()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.ContractsAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.InfrastructureAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Contracts_Should_Not_DependOn_Endpoints()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.ContractsAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.EndpointsAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }

    [Fact]
    public void Contracts_Should_Not_DependOn_BuildingBlocks_API()
    {
        ArchRuleDefinition
            .Types().That().ResideInAssembly(ArchitectureRegistry.ContractsAssembly)
            .Should().NotDependOnAny(
                ArchRuleDefinition.Types().That().ResideInAssembly(ArchitectureRegistry.BuildingBlocksApiAssembly))
            .Check(ArchitectureRegistry.Architecture);
    }
}