using System;
using Ninject;
using SimpleInjector;

namespace QueryCaching
{
    public class Mediator : IMediator
    {
        private readonly IKernel _container;

        public Mediator(IKernel container)
        {
            _container = container;
        }

        public TResponse Request<TResponse>(IQuery<TResponse> query)
        {
            var makeGenericType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
            dynamic queryHandler = _container.Get(makeGenericType);
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