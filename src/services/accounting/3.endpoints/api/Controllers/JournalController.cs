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

        [HttpPost("{id:guid}/post")]
        public async Task<IActionResult> PostJournal([FromRoute] Guid id)
            => Create(await _bus.SendCommandAsync<Unit>(new PostJournalCommand { JournalEntryId = id }));
    }
}
