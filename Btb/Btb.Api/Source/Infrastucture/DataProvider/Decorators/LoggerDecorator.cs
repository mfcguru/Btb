using Btb.Api.Source.Domain.Features.AddCustomer;
using Btb.Api.Source.Domain.Features.EditCustomer;
using Btb.Api.Source.Domain.Features.GetCustomer;
using Btb.Api.Source.Domain.Features.GetCustomers;
using System.Runtime.CompilerServices;

namespace Btb.Api.Source.Infrastucture.DataProvider.Decorators
{
    public class LoggerDecorator : BaseDecorator
    {
        private readonly ILogger<LoggerDecorator> logger;
        public LoggerDecorator(IDataProvider dataProvider, ILogger<LoggerDecorator> logger) 
            : base(dataProvider)
        {
            this.logger = logger;
        }

        private void LogInformation([CallerMemberName]string caller = "") => logger.LogInformation(caller);

        public override async Task AddCustomer(AddCustomerParameters parameters, CancellationToken cancellationToken)
        {
            LogInformation();

            await base.AddCustomer(parameters, cancellationToken);
        }

        public override async Task DeleteCustomer(int customerId, CancellationToken cancellationToken)
        {
            LogInformation();

            await base.DeleteCustomer(customerId, cancellationToken);
        }

        public override async Task DeleteDependent(int dependentId, CancellationToken cancellationToken)
        {
            LogInformation();

            await base.DeleteDependent(dependentId, cancellationToken);
        }

        public override async Task EditCustomer(int customerId, EditCustomerParameters parameters, CancellationToken cancellationToken)
        {
            LogInformation();

            await base.EditCustomer(customerId, parameters, cancellationToken);
        }

        public override async Task<GetCustomerResult> GetCustomer(int customerId, CancellationToken cancellationToken)
        {
            LogInformation();

            var result = await base.GetCustomer(customerId, cancellationToken);

            return result;
        }

        public override async Task<GetCustomersResult> GetCustomers(GetCustomersFilter filter, CancellationToken cancellationToken)
        {
            LogInformation();

            var result = await base.GetCustomers(filter, cancellationToken);

            return result;
        }
    }
}
