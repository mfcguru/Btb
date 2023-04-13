namespace Btb.Api.Source.Domain.Features.GetCustomers
{
    public enum GetCustomersFilter
    {
        NoFilter = 1,
        OnlyCustomersWithDependents,
        OnlyCustomersWithoutDependents
    }
}
