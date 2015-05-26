using Ninject;

namespace QueryCaching
{
    class Program
    {
        static void Main()
        {
            //var container = new Container();
            //container.Register<IMediator, Mediator>();
            //container.RegisterManyForOpenGeneric(typeof(IQueryHandler<,>), typeof(IQueryHandler<,>).Assembly);
            //container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(CacheQueryProxyHandler<,>));

            var container = new StandardKernel();
            container.Bind<IMediator>().To<Mediator>();
            //container.Bind(
            //    x =>
            //        x.FromThisAssembly()
            //            .SelectAllClasses()
            //            .InheritedFrom(typeof(IQueryHandler<,>))
            //            .BindSingleInterface());

            container.Bind(typeof(IQueryHandler<PersonQuery, Person>))
                .To(typeof(PersonQueryHandler))
                .WhenInjectedInto(typeof (CacheQueryProxyHandler<,>));
            
            container.Bind(typeof(IQueryHandler<,>)).To(typeof(CacheQueryProxyHandler<,>));


            var handler = new CacheQueryProxyHandler<PersonQuery, Person>(new PersonQueryHandler());


            var mediator = container.Get<IMediator>();
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
