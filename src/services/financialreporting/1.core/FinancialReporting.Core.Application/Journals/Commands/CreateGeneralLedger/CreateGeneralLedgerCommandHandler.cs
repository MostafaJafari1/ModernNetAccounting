using BuildingBlocks.Application.Common;
using BuildingBlocks.Application.CQRS.Commands;
using BuildingBlocks.Common;
using FinancialReporting.Core.RequestResponse.Journals.Commands;
using Microsoft.Extensions.Logging;

namespace FinancialReporting.Core.Application.Journals.Commands.CreateGeneralLedger;

public class CreateGeneralLedgerCommandHandler(
     ILogger<CreateGeneralLedgerCommandHandler> logger
    ) :
     BaseCommandHandler<CreateGeneralLedgerCommand, Unit>(logger) 
{
    protected override async Task<Result<Unit>> HandleAsync(
        CreateGeneralLedgerCommand command,
        CancellationToken cancellationToken)
    {

      
        return Result<Unit>.Success(Unit.Value);
    }
}
