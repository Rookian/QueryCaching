using System;
using System.Collections.Concurrent;

namespace QueryCaching
{
    public class QueryCacheConfiguration
    {
        public static ConcurrentDictionary<Type, QueryCacheConfigurationItem> QueryCacheConfigurationItems { get; set; }

        public QueryCacheConfiguration()
        {
            QueryCacheConfigurationItems = new ConcurrentDictionary<Type, QueryCacheConfigurationItem>();
        }
    }
}