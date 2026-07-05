global using Microsoft.Extensions.Logging;

global using Wolverine;

global using BuildingBlocks.Common;
global using BuildingBlocks.Application.Common;
global using BuildingBlocks.Application.CQRS.Commands;
global using BuildingBlocks.Integrations.Wolverine;

global using Accounting.Core.Contracts.Journals;
global using Accounting.Core.Domain.Journals;
global using Accounting.Core.RequestResponse.Accounts.Commands;
global using Accounting.Core.RequestResponse.Journals.Commands;

global using Accounting.Core.Contracts.Accounts;
global using Accounting.Core.Domain.Accounts;
global using Accounting.Core.Domain.Accounts.Enums;
global using Accounting.Core.Domain.Accounts.Services;
global using Accounting.Core.Domain.Accounts.ValueObjects;
global using Accounting.Infrastructure.Data.Sql.Write;
global using BuildingBlocks.Domain.Primitives;
global using BuildingBlocks.Common.Errors;
global using Accounting.Core.Resources.Messages;

global using Accounting.Core.Contracts.Journals.IntegrationEvents;
global using Accounting.Core.Domain.Journals.Events;
global using BuildingBlocks.Application.CQRS.Events;
