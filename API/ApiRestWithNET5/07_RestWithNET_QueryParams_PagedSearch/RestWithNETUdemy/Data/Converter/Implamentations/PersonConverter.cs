using RestWithNETUdemy.Data.Converter.Contract;
using RestWithNETUdemy.Data.VO;
using RestWithNETUdemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithNETUdemy.Data.Converter.Implamentations
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public PersonVO Parse(Person origem)
        {
            if (origem == null) return null;
            return new PersonVO
            {
                Id = origem.Id,
                FirstName = origem.FirstName,
                LastName = origem.LastName,
                Address = origem.Address,
                Gender = origem.Gender,
                Enabled = origem.Enabled
            };
        }

        public Person Parse(PersonVO origem)
        {
            if (origem == null) return null;
            return new Person
            {
                Id = origem.Id,
                FirstName = origem.FirstName,
                LastName = origem.LastName,
                Address = origem.Address,
                Gender = origem.Gender,
                Enabled = origem.Enabled
            };
        }

        public List<Person> Parse(List<PersonVO> origem)
        {
            if (origem == null) return null;
            return origem.Select(item => Parse(item)).ToList();
        }

        public List<PersonVO> Parse(List<Person> origem)
        {
            if (origem == null) return null;
            return origem.Select(item => Parse(item)).ToList();
        }
    }
}
