using System.Threading;

namespace QueryCaching
{
    public class PersonQueryHandler : IQueryHandler<PersonQuery, Person>
    {
        public Person Handle(PersonQuery query)
        {
            Thread.Sleep(1000);
            var id = query.Id;
            return new Person { Id = id, Name = "Test" };
        }
    }
}