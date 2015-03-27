using System.Threading;

namespace QueryCaching
{
    public class PersonQueryHandler : IQueryHandler<PersonQuery, Person>
    {
        public Person Handle(PersonQuery query)
        {
            var id = query.Id;
            Thread.Sleep(10000);
            return new Person { Id = id, Name = "Test" };
        }
    }
}