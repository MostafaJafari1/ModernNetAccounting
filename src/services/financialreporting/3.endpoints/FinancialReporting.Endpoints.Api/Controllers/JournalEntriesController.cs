using BuildingBlocks.API.Controllers;
using BuildingBlocks.Application.Common;
using FinancialReporting.Core.RequestResponse.Journals.Commands;
using Microsoft.AspNetCore.Mvc;
using Wolverine;
using BuildingBlocks.Integrations.Wolverine;

namespace Accounting.Endpoints.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JournalEntriesController : BaseApiController
    {
        private readonly IMessageBus _bus;

        public JournalEntriesController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJournalEntry([FromBody] CreateJournalEntryCommand command)
            => Create(await _bus.SendCommandAsync<Unit>(command));
    }
}