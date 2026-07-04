using Accounting.Core.RequestResponse.Accounts.Commands;
using Accounting.Core.RequestResponse.Journals.Commands;
using BuildingBlocks.API.Controllers;
using BuildingBlocks.Application.Common;
using BuildingBlocks.Integrations.Wolverine;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace Accounting.Endpoints.Api.Controllers;

public class AccountController : BaseApiController
{
    private readonly IMessageBus _bus;
    public AccountController(IMessageBus bus)
    {
        _bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
     => Create(await _bus.SendCommandAsync<Unit>(command));
}
