using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Btb.Api.Source.Domain.Features.AddCustomer
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddCustomerController : ControllerBase
    {
        private readonly IMediator mediator;
        public AddCustomerController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddCustomer(AddCustomerParameters parameters)
        {
            await mediator.Send(new AddCustomerCommand(parameters));

            return Ok();
        }
    }
}
