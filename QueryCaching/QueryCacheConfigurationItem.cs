using System;

namespace QueryCaching
{
    public class QueryCacheConfigurationItem
    {
        public Type QueryType { get; set; }
        public DateTime AbsolutExpiration { get; set; }
    }
}