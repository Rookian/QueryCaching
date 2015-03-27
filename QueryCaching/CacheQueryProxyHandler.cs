﻿using System.Collections.Concurrent;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace QueryCaching
{
    public class CacheQueryProxyHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _next;

        private static readonly ConcurrentDictionary<string, TResult> Cache = new ConcurrentDictionary<string, TResult>();

        public CacheQueryProxyHandler(IQueryHandler<TQuery, TResult> next)
        {
            _next = next;
        }

        public TResult Handle(TQuery query)
        {
            var key = GenerateKey(query);

            TResult result;
            if (Cache.TryGetValue(key, out result))
            {
                return result;
            }

            result = _next.Handle(query);
            Cache[key] = result;
            return result;
        }

        private static string GenerateKey(TQuery query)
        {
            return query.GetType().FullName + "_" + JsonConvert.SerializeObject(query);
        }
    }
}