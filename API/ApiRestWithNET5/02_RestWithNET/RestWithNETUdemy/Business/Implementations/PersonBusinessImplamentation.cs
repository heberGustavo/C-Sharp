using RestWithNETUdemy.Data.Converter.Implamentations;
using RestWithNETUdemy.Data.VO;
using RestWithNETUdemy.Model;
using RestWithNETUdemy.Repository;
using System.Collections.Generic;

namespace RestWithNETUdemy.Business.Implementations
{
    public class PersonBusinessImplamentation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplamentation(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Create(PersonVO personVO)
        {
            var entity = _converter.Parse(personVO);
            entity = _repository.Create(entity);
            return _converter.Parse(entity);
        }

        public PersonVO Update(PersonVO personVO)
        {
            var entity = _converter.Parse(personVO);
            entity = _repository.Update(entity);
            return _converter.Parse(entity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    }
}
