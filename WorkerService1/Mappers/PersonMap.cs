using CsvHelper.Configuration;
using WorkerService1.Models;

namespace WorkerService1.Mappers
{
    public sealed class PersonMap: ClassMap<Person>
    {
        public PersonMap()
        {
            //Map(x => x.Id).Name("Id");
            Map(x => x.Firstname).Name("Firstname");
            Map(x => x.Surname).Name("Surname");
            Map(x => x.Sex).Name("Sex");
            Map(x => x.Age).Name("Age");
            Map(x => x.Status).Name("Status");
        }
    }
}