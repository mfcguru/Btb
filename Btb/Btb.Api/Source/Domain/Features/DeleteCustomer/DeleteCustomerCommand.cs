using Btb.Api.Source.Infrastucture.DataProvider;
using MediatR;
using Microsoft.Extensions.Options;

namespace Btb.Api.Source.Domain.Features.DeleteDependent
{
    public class DeleteCustomerCommand : IRequest
    {
        private readonly int customerId;
        public DeleteCustomerCommand(int customerId) => this.customerId = customerId;

        public class Handler : IRequestHandler<DeleteCustomerCommand>
        {
            private readonly IDataProvider dataProvider;
            public Handler(DataProiderFactory factory, IOptions<AppSettings> appSettings)
            {
                dataProvider = factory.CreateInstance(appSettings.Value.DataProiderType);
            }

            public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                await dataProvider.DeleteCustomer(request.customerId, cancellationToken);
            }
        }
    }
}
