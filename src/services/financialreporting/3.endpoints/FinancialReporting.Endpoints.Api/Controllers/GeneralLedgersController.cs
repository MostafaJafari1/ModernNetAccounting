using BuildingBlocks.API.Controllers;
using BuildingBlocks.Application.Common;
using FinancialReporting.Core.RequestResponse.Journals.Commands;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using BuildingBlocks.Integrations.Wolverine;

namespace Accounting.Endpoints.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralLedgersController : BaseApiController
    {
        private readonly IMessageBus _bus;

        public GeneralLedgersController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGeneralLedger([FromBody] CreateGeneralLedgerCommand command)
            => Create(await _bus.SendCommandAsync<Unit>(command));
    }
}