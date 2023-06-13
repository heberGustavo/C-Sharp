using RestWithNETUdemy.Model;
using RestWithNETUdemy.Model.Context;
using System;
using System.Linq;

namespace RestWithNETUdemy.Repository
{
    public class PersonRepository : Base.Repository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context) { }

        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id))) return null;

            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if(user != null)
            {
                try
                {
                    user.Enabled = true;
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            return user;
        }
    }
}
