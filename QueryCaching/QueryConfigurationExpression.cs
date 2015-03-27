using System;
using System.Collections.Concurrent;

namespace QueryCaching
{

    public class QueryConfigurationExpression
    {
        public QueryCacheConfiguration QueryCacheConfiguration { get; private set; }

        public QueryConfigurationExpression()
        {
            QueryCacheConfiguration = new QueryCacheConfiguration();
        }

        public void CacheQuery<TQuery>()
        {
            QueryCacheConfiguration.QueryCacheConfigurationItems[typeof(TQuery)] =
            new QueryCacheConfigurationItem
            {
                QueryType = typeof(TQuery)
            };
        }
    }

    public class QueryCacheConfiguration
    {
        public static ConcurrentDictionary<Type, QueryCacheConfigurationItem> QueryCacheConfigurationItems { get; set; }

        public QueryCacheConfiguration()
        {
            QueryCacheConfigurationItems = new ConcurrentDictionary<Type, QueryCacheConfigurationItem>();
        }
    }

    public class QueryCacheConfigurationItem
    {
        public Type QueryType { get; set; }
        public DateTime AbsolutExpiration { get; set; }
    }
}