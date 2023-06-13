using RestWithNETUdemy.Data.Converter.Implamentations;
using RestWithNETUdemy.Data.VO;
using RestWithNETUdemy.Hypermedia.Utils;
using RestWithNETUdemy.Model;
using RestWithNETUdemy.Repository;
using System.Collections.Generic;

namespace RestWithNETUdemy.Business.Implementations
{
    public class PersonBusinessImplamentation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplamentation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string firstName, string sortDirection, int pageSize, int page)
        {
            var sort = !string.IsNullOrWhiteSpace(sortDirection) && sortDirection.Equals("asc") ? "asc" : "desc";
            var size = pageSize < 1 ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @" SELECT * FROM person p WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(firstName)) query += $" AND p.first_name like '%{firstName}%' ";
            query += $" ORDER BY p.first_name {sort} LIMIT {size} OFFSET {offset} ";

            var countQuery = " SELECT COUNT(*) FROM person p WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(firstName)) countQuery += $" AND p.first_name like '%{firstName}%' ";

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
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

        public PersonVO Disabled(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    }
}
