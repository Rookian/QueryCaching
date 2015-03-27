using System;
using SimpleInjector;

namespace QueryCaching
{
    public class Mediator : IMediator
    {
        private readonly Container _container;

        public Mediator(Container container)
        {
            _container = container;
        }

        public TResponse Request<TResponse>(IQuery<TResponse> query)
        {
            var makeGenericType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
            dynamic queryHandler = _container.GetInstance(makeGenericType);
            return queryHandler.Handle((dynamic)query);
        }

        public void Send(object command)
        {
        }

        public void ConfigureQueryCache(Action<QueryConfigurationExpression> queryConfigurationExpression)
        {
            var configurationExpression = new QueryConfigurationExpression();
            queryConfigurationExpression(configurationExpression);

        }
    }
}