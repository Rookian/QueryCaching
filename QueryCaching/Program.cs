using SimpleInjector;
using SimpleInjector.Extensions;

namespace QueryCaching
{
    class Program
    {
        static void Main()
        {
            var container = new Container();
            container.Register<IMediator, Mediator>();
            container.RegisterManyForOpenGeneric(typeof(IQueryHandler<,>), typeof(IQueryHandler<,>).Assembly);
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(CacheQueryProxyHandler<,>));

            var mediator = container.GetInstance<IMediator>();
            mediator.ConfigureQueryCache(configure =>
            {
                configure.Cache<PersonQuery>();
            });


            var person = mediator.Request(new PersonQuery { Id = 1 });
            var person1 = mediator.Request(new PersonQuery { Id = 1 });

            var person2 = mediator.Request(new PersonQuery { Id = 5 });

        }
    }
}
