using RestWithNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithNETUdemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
        List<Person> FindByName(string firstName, string lastName);
    }
}
