using BuildingBlocks.API.Controllers;
using BuildingBlocks.Application.Common;
using BuildingBlocks.Common;
using BuildingBlocks.Integrations.Wolverine;
using FinancialReporting.Core.RequestResponse.Journals.Commands;
using FinancialReporting.Core.RequestResponse.Journals.Queries;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

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

        [HttpGet]
        public async Task<IActionResult> GetJournalEntries([FromQuery] GetJournalEntriesQuery query)
       => Get(await _bus.SendQueryAsync(query));
    }
}