using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Btb.Api.Source.Domain.Features.DeleteDependent
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteDependentController : ControllerBase
    {
        private readonly IMediator mediator;
        public DeleteDependentController(IMediator mediator) => this.mediator = mediator;

        [HttpDelete("{dependentId}")]
        public async Task<IActionResult> DeleteDependent(int dependentId)
        {
            await mediator.Send(new DeleteDependentCommand(dependentId));

            return Ok();
        }
    }
}
