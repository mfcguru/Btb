using Btb.Api.Source.Domain.Features.GetCustomer;
using Btb.Api.Source.Domain.Features.GetCustomers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Btb.Api.Source.Infrastucture.DataProvider.Decorators
{
    public class CacheDecorator : BaseDecorator
    {
        private readonly IMemoryCache cache;
        private readonly int expiration;

        public CacheDecorator(IDataProvider dataProvider, IMemoryCache cache, IOptions<AppSettings> appSettings)
            : base(dataProvider)
        {
            this.cache = cache;

            expiration = appSettings.Value.CacheExpiration;
        }

        public override async Task<GetCustomerResult> GetCustomer(int customerId, CancellationToken cancellationToken)
        {
            string key = "GET_CUSTOMER";

            var result = await cache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiration);

                return base.GetCustomer(customerId, cancellationToken);
            });

            return result;
        }

        public override async Task<GetCustomersResult> GetCustomers(GetCustomersFilter filter, CancellationToken cancellationToken)
        {
            string key = "GET_CUSTOMERS";

            var result = await cache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiration);

                return base.GetCustomers(filter, cancellationToken);
            });

            return result;
        }
    }
}
