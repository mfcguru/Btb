using Btb.Api.Source.Domain.Features.AddCustomer;
using Btb.Api.Source.Domain.Features.EditCustomer;
using Btb.Api.Source.Domain.Features.GetCustomer;
using Btb.Api.Source.Domain.Features.GetCustomers;

namespace Btb.Api.Source.Infrastucture.DataProvider
{
    public interface IDataProvider
    {
        Task AddCustomer(AddCustomerParameters parameters, CancellationToken cancellationToken);
        Task DeleteCustomer(int customerId, CancellationToken cancellationToken);
        Task DeleteDependent(int dependentId, CancellationToken cancellationToken);
        Task EditCustomer(int customerId, EditCustomerParameters parameters, CancellationToken cancellationToken);
        Task<GetCustomerResult> GetCustomer(int customerId, CancellationToken cancellationToken);
        Task<GetCustomersResult> GetCustomers(GetCustomersFilter filter, CancellationToken cancellationToken);
    }
}
