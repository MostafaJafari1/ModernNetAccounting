using Accounting.Core.RequestResponse.Journals.Commands;
using BuildingBlocks.API.Controllers;
using BuildingBlocks.Application.Common;
using BuildingBlocks.Integrations.Wolverine;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace Accounting.Endpoints.Api.Controllers
{
    public class JournalController : BaseApiController
    {
        private readonly IMessageBus _bus;
        public JournalController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJournal([FromBody] CreateJournalCommand command)
         => Create(await _bus.SendCommandAsync<Unit>(command));
    }
}
