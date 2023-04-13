using Btb.Api.Source.Infrastucture.DataProvider;
using MediatR;
using Microsoft.Extensions.Options;

namespace Btb.Api.Source.Domain.Features.DeleteDependent
{
    public class DeleteDependentCommand : IRequest
    {
        private readonly int dependentId;
        public DeleteDependentCommand(int dependentId) => this.dependentId = dependentId;

        public class Handler : IRequestHandler<DeleteDependentCommand>
        {
            private readonly IDataProvider dataProvider;
            public Handler(DataProiderFactory factory, IOptions<AppSettings> appSettings)
            {
                dataProvider = factory.CreateInstance(appSettings.Value.DataProiderType);
            }

            public async Task Handle(DeleteDependentCommand request, CancellationToken cancellationToken)
            {
                await dataProvider.DeleteDependent(request.dependentId, cancellationToken);
            }
        }
    }
}
