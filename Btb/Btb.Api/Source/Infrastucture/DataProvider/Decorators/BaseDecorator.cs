using Btb.Api.Source.Domain.Features.AddCustomer;
using Btb.Api.Source.Domain.Features.EditCustomer;
using Btb.Api.Source.Domain.Features.GetCustomer;
using Btb.Api.Source.Domain.Features.GetCustomers;

namespace Btb.Api.Source.Infrastucture.DataProvider.Decorators
{
    public abstract class BaseDecorator : IDataProvider
    {
        private readonly IDataProvider dataProvider;
        protected BaseDecorator(IDataProvider dataProvider) => this.dataProvider = dataProvider;

        public virtual async Task AddCustomer(AddCustomerParameters parameters, CancellationToken cancellationToken)
        {
            await dataProvider.AddCustomer(parameters, cancellationToken);
        }

        public virtual async Task DeleteCustomer(int customerId, CancellationToken cancellationToken)
        {
            await dataProvider.DeleteCustomer(customerId, cancellationToken);
        }

        public virtual async Task DeleteDependent(int dependentId, CancellationToken cancellationToken)
        {
            await dataProvider.DeleteDependent(dependentId, cancellationToken);
        }

        public virtual async Task EditCustomer(int customerId, EditCustomerParameters parameters, CancellationToken cancellationToken)
        {
            await dataProvider.EditCustomer(customerId, parameters, cancellationToken);
        }

        public virtual async Task<GetCustomerResult> GetCustomer(int customerId, CancellationToken cancellationToken)
        {
            var result = await dataProvider.GetCustomer(customerId, cancellationToken);

            return result;
        }

        public virtual async Task<GetCustomersResult> GetCustomers(GetCustomersFilter filter, CancellationToken cancellationToken)
        {
            var result = await dataProvider.GetCustomers(filter, cancellationToken);

            return result;
        }
    }
}
