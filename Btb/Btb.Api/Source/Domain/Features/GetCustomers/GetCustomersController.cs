using Btb.Api.Source.Domain.Features.AddCustomer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Btb.Api.Source.Domain.Features.GetCustomers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCustomersController : ControllerBase
    {
        private readonly IMediator mediator;
        public GetCustomersController(IMediator mediator) => this.mediator = mediator;

        [HttpGet("{filter}")]
        public async Task<IActionResult> GetCustomers(GetCustomersFilter filter = GetCustomersFilter.NoFilter)
        {
            var result = await mediator.Send(new GetCustomersCommand(filter));

            return Ok(result);
        }
    }
}
