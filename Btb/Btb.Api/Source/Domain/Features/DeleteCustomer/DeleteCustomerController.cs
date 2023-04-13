using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Btb.Api.Source.Domain.Features.DeleteDependent
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteCustomerController : ControllerBase
    {
        private readonly IMediator mediator;
        public DeleteCustomerController(IMediator mediator) => this.mediator = mediator;


        [HttpDelete("{dependentId}")]
        public async Task<IActionResult> DeleteCustomer(int dependentId)
        {
            await mediator.Send(new DeleteCustomerCommand(dependentId));

            return Ok();
        }
    }
}
