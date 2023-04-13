using Btb.Api.Source.Domain.Entities;
using Btb.Api.Source.Domain.Enums;
using Btb.Api.Source.Domain.Features.AddCustomer;
using Btb.Api.Source.Domain.Features.EditCustomer;
using Btb.Api.Source.Domain.Features.GetCustomer;
using Btb.Api.Source.Domain.Features.GetCustomers;
using Microsoft.EntityFrameworkCore;

namespace Btb.Api.Source.Infrastucture.DataProvider
{
    public class EntityFrameworkDataProider : IDataProvider
    {
        private readonly DataContext context;
        public EntityFrameworkDataProider(DataContext context) => this.context = context;

        public async Task AddCustomer(AddCustomerParameters parameters, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                CustomerType = (int)parameters.CustomerType,
                CustomerDetails = parameters.CustomerDetails
                    .Select(customerDetail => new CustomerDetail
                    {
                        MetaKey = customerDetail.MetaKey,
                        MetaValue = customerDetail.MetaValue
                    }).ToList(),
                Dependents = parameters.Dependents
                    .Select(dependent => new Dependent
                    {
                        DependentDetails = dependent.DependentDetails
                            .Select(dependentDetail => new DependentDetail
                            {
                                MetaKey = dependentDetail.MetaKey,
                                MetaValue = dependentDetail.MetaValue
                            }).ToList()
                    }).ToList()
            };

            context.Customers.Add(customer);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCustomer(int customerId, CancellationToken cancellationToken)
        {
            var customer = await context.Customers.FindAsync(customerId, cancellationToken);
            if (customer != null)
            {
                customer.IsActive = false;
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteDependent(int dependentId, CancellationToken cancellationToken)
        {
            var dependent = await context.Dependents.FindAsync(dependentId, cancellationToken);
            if (dependent != null)
            {
                dependent.IsActive = false;
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task EditCustomer(int customerId, EditCustomerParameters parameters, CancellationToken cancellationToken)
        {
            var customer = await context.Customers
                    .Include(o => o.CustomerDetails)
                    .Include(o => o.Dependents)
                    .ThenInclude(o => o.DependentDetails)
                    .SingleOrDefaultAsync(o => o.CustomerID == customerId, cancellationToken);

            if (customer != null)
            {
                customer.CustomerDetails.Clear();
                customer.Dependents.Clear();

                customer.CustomerType = (int)parameters.CustomerType;
                customer.CustomerDetails = parameters.CustomerDetails
                    .Select(customerDetail => new CustomerDetail
                    {
                        MetaKey = customerDetail.MetaKey,
                        MetaValue = customerDetail.MetaValue
                    }).ToList();
                customer.Dependents = parameters.Dependents
                    .Select(dependent => new Dependent
                    {
                        DependentDetails = dependent.DependentDetails
                            .Select(dependentDetail => new DependentDetail
                            {
                                MetaKey = dependentDetail.MetaKey,
                                MetaValue = dependentDetail.MetaValue
                            }).ToList()
                    }).ToList();

                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<GetCustomerResult> GetCustomer(int customerId, CancellationToken cancellationToken)
        {
            var result = await context.Customers
                    .Include(o => o.CustomerDetails)
                    .Include(o => o.Dependents)
                    .ThenInclude(o => o.DependentDetails)
                    .AsNoTracking()
                    .Select(o => new GetCustomerResult
                    {
                        CustomerId = o.CustomerID,
                        CustomerType = (CustomerType)o.CustomerType,
                        IsActive = o.IsActive,
                        CreatedDate = o.CreatedDateTime,
                        CustomerDetails = o.CustomerDetails
                                .Select(o => new GetCustomerResult.CustomerDetail
                                {
                                    CustomerDetailId = o.CustomerDetailID,
                                    CustomerId = o.CustomerID,
                                    MetaKey = o.MetaKey,
                                    MetaValue = o.MetaValue
                                }).ToList(),
                        Dependents = o.Dependents
                                .Where(o => o.IsActive)
                                .Select(o => new GetCustomerResult.Dependent
                                {
                                    DependentId = o.DependentID,
                                    DependentDetails = o.DependentDetails
                                        .Select(o => new GetCustomerResult.Dependent.DependentDetail
                                        {
                                            DependentDetailId = o.DependentDetailID,
                                            DependentId = o.DependentDetailID,
                                            MetaKey = o.MetaKey,
                                            MetaValue = o.MetaValue
                                        }).ToList(),
                                    IsActive = o.IsActive,
                                    CreatedDate = o.CreatedDateTime,
                                }).ToList(),
                    })
                    .SingleOrDefaultAsync(o => o.CustomerId == customerId && o.IsActive, cancellationToken);

            return result;
        }

        public async Task<GetCustomersResult> GetCustomers(GetCustomersFilter filter, CancellationToken cancellationToken)
        {
            var query = context.Customers
                    .Include(o => o.CustomerDetails)
                    .Include(o => o.Dependents)
                    .ThenInclude(o => o.DependentDetails)
                    .Where(o => o.IsActive)
                    .AsNoTracking();

            switch (filter)
            {
                case GetCustomersFilter.OnlyCustomersWithDependents:
                    query = query.Where(o => o.Dependents.Any());
                    break;
                case GetCustomersFilter.OnlyCustomersWithoutDependents:
                    query = query.Where(o => !o.Dependents.Any());
                    break;
            }

            var result = new GetCustomersResult
            {
                Customers = await query
                    .Select(o => new GetCustomersResult.Customer
                    {
                        CustomerId = o.CustomerID,
                        CustomerType = (CustomerType)o.CustomerType,
                        IsActive = o.IsActive,
                        CreatedDate = o.CreatedDateTime,
                        CustomerDetails = o.CustomerDetails
                            .Select(o => new GetCustomersResult.Customer.CustomerDetail
                            {
                                CustomerDetailId = o.CustomerDetailID,
                                CustomerId = o.CustomerID,
                                MetaKey = o.MetaKey,
                                MetaValue = o.MetaValue
                            }).ToList(),
                        Dependents = o.Dependents
                            .Where(o => o.IsActive)
                            .Select(o => new GetCustomersResult.Customer.Dependent
                            {
                                DependentId = o.DependentID,
                                DependentDetails = o.DependentDetails
                                    .Select(o => new GetCustomersResult.Customer.Dependent.DependentDetail
                                    {
                                        DependentDetailId = o.DependentDetailID,
                                        DependentId = o.DependentDetailID,
                                        MetaKey = o.MetaKey,
                                        MetaValue = o.MetaValue
                                    }).ToList(),
                                IsActive = o.IsActive,
                                CreatedDate = o.CreatedDateTime,
                            }).ToList(),
                    })
                    .ToListAsync(cancellationToken)
            };

            return result;
        }
    }
}
