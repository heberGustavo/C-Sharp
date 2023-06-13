using RestWithNETUdemy.Model;

namespace RestWithNETUdemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
    }
}
