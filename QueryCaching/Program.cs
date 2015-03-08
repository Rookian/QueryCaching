using SimpleInjector.Extensions;
using Container = SimpleInjector.Container;

namespace QueryCaching
{
    class Program
    {
        static void Main()
        {
            var container = new Container();
            container.Register<IMediator, Mediator>();
            container.RegisterManyForOpenGeneric(typeof (IQueryHandler<,>), typeof (IQueryHandler<,>).Assembly);

            var mediator = container.GetInstance<IMediator>();

            var person = mediator.Request(new Query1 {Id = 1});

        }
    }

    public interface IQuery<TResult>
    {

    }

    interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }


    public interface IMediator
    {
        TResponse Request<TResponse>(IQuery<TResponse> query);
        void Send(object command);
    }

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
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Query1 : IQuery<Person>
    {
        public int Id { get; set; }
    }

    public class PersonQueryHandler : IQueryHandler<Query1, Person>
    {
        public PersonQueryHandler()
        {
            
        }
        public Person Handle(Query1 query)
        {
            var id = query.Id;
            return new Person { Id = 1, Name = "Test" };
        }
    }

    
}
