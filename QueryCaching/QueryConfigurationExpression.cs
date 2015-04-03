namespace QueryCaching
{

    public class QueryConfigurationExpression
    {
        public QueryCacheConfiguration QueryCacheConfiguration { get; private set; }

        public QueryConfigurationExpression()
        {
            QueryCacheConfiguration = new QueryCacheConfiguration();
        }

        
        public void Cache<TQuery>()
        {
            var item = new QueryCacheConfigurationItem
            {
                QueryType = typeof(TQuery)
            };

            QueryCacheConfiguration.QueryCacheConfigurationItems[typeof(TQuery)] = item;
        }
    }
}