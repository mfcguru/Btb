using Btb.Api.Source.Infrastucture.DataProvider;
using MediatR;
using Microsoft.Extensions.Options;

namespace Btb.Api.Source.Domain.Features.GetCustomers
{
    public class GetCustomersCommand : IRequest<GetCustomersResult>
    {
        private readonly GetCustomersFilter filter;
        public GetCustomersCommand(GetCustomersFilter filter) => this.filter = filter;

        public class Handler : IRequestHandler<GetCustomersCommand, GetCustomersResult>
        {
            private readonly IDataProvider dataProvider;
            public Handler(DataProiderFactory factory, IOptions<AppSettings> appSettings)
            {
                dataProvider = factory.CreateInstance(appSettings.Value.DataProiderType);
            }

            public async Task<GetCustomersResult> Handle(GetCustomersCommand request, CancellationToken cancellationToken)
            {
                var result = await dataProvider.GetCustomers(request.filter, cancellationToken);

                return result;
            }
        }
    }
}
