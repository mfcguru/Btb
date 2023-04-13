using Btb.Api.Source.Infrastucture.DataProvider;
using MediatR;
using Microsoft.Extensions.Options;

namespace Btb.Api.Source.Domain.Features.AddCustomer
{
    public class AddCustomerCommand : IRequest
    {
        private readonly AddCustomerParameters parameters;
        public AddCustomerCommand(AddCustomerParameters parameters) => this.parameters = parameters;

        public class Handler : IRequestHandler<AddCustomerCommand>
        {
            private readonly IDataProvider dataProvider;
            public Handler(DataProiderFactory factory, IOptions<AppSettings> appSettings)
            {
                dataProvider = factory.CreateInstance(appSettings.Value.DataProiderType);
            }

            public async Task Handle(AddCustomerCommand request, CancellationToken cancellationToken)
            {
                await dataProvider.AddCustomer(request.parameters, cancellationToken);
            }
        }
    }
}
