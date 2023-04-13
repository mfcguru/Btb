using Btb.Api.Source.Infrastucture.DataProvider;
using MediatR;
using Microsoft.Extensions.Options;

namespace Btb.Api.Source.Domain.Features.GetCustomer
{
    public class GetCustomerCommand : IRequest<GetCustomerResult>
    {
        private readonly int customerId;
        public GetCustomerCommand(int customerId) => this.customerId = customerId;

        public class Handler : IRequestHandler<GetCustomerCommand, GetCustomerResult>
        {
            private readonly IDataProvider dataProvider;
            public Handler(DataProiderFactory factory, IOptions<AppSettings> appSettings)
            {
                dataProvider = factory.CreateInstance(appSettings.Value.DataProiderType);
            }

            public async Task<GetCustomerResult> Handle(GetCustomerCommand request, CancellationToken cancellationToken)
            {
                var result = await dataProvider.GetCustomer(request.customerId, cancellationToken);

                return result;
            }
        }
    }
}
