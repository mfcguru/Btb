using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Btb.Api.Source.Domain.Features.EditCustomer
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditCustomerController : ControllerBase
    {
        private readonly IMediator mediator;
        public EditCustomerController(IMediator mediator) => this.mediator = mediator;

        [HttpPut("{customerId}")]
        public async Task<IActionResult> EditCustomer(int customerId, [FromBody]EditCustomerParameters parameters)
        {
            await mediator.Send(new EditCustomerCommand(customerId, parameters));

            return Ok();
        }
    }
}
