using System;

namespace QueryCaching
{
    public interface IMediator
    {
        TResponse Request<TResponse>(IQuery<TResponse> query);
        void Send(object command);
        void ConfigureQueryCache(Action<QueryConfigurationExpression> queryConfigurationExpression);
    }
}