using Btb.Api.Source.Infrastucture.DataProvider;

namespace Btb.Api
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public DataProiderType DataProiderType { get; set; }
        public int CacheExpiration  { get; set; }
    }
}
