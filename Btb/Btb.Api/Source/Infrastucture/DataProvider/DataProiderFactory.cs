using Btb.Api.Source.Infrastucture.DataProvider.Decorators;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Btb.Api.Source.Infrastucture.DataProvider
{
    public sealed class DataProiderFactory
    {
        private readonly IServiceProvider serviceProvider;
        public DataProiderFactory(IServiceProvider serviceProvider) 
            => this.serviceProvider = serviceProvider;

        public IDataProvider CreateInstance(DataProiderType dataProiderType)
        {
            IDataProvider instance;

            switch (dataProiderType)
            {
                case DataProiderType.EntityFramework:
                    instance = serviceProvider.GetService<EntityFrameworkDataProider>();
                    break;
                default:
                    throw new ArgumentException("Invalid data provider type", nameof(dataProiderType));
            }

            var logger = serviceProvider.GetService<ILogger<LoggerDecorator>>();
            instance = new LoggerDecorator(instance, logger);

            var cache = serviceProvider.GetService<IMemoryCache>();
            var appSettings = serviceProvider.GetService<IOptions<AppSettings>>();
            instance = new CacheDecorator(instance, cache, appSettings);

            return instance;
        }
    }
}
