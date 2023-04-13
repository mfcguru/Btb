using Btb.Api.Source.Infrastucture.DataProvider;
using MediatR;
using Microsoft.Extensions.Options;

namespace Btb.Api.Source.Domain.Features.EditCustomer
{
    public class EditCustomerCommand : IRequest
    {
        private readonly int customerId;
        private readonly EditCustomerParameters parameters;
        public EditCustomerCommand(int customerId, EditCustomerParameters parameters)
        {
            this.customerId = customerId;
            this.parameters = parameters;
        }

        public class Handler : IRequestHandler<EditCustomerCommand>
        {
            private readonly IDataProvider dataProvider;
            public Handler(DataProiderFactory factory, IOptions<AppSettings> appSettings)
            {
                dataProvider = factory.CreateInstance(appSettings.Value.DataProiderType);
            }

            public async Task Handle(EditCustomerCommand request, CancellationToken cancellationToken)
            {
                await dataProvider.EditCustomer(request.customerId, request.parameters, cancellationToken);
            }
        }
    }
}
