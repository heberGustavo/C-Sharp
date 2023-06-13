using RestWithNETUdemy.Data.VO;
using RestWithNETUdemy.Hypermedia.Utils;
using System.Collections.Generic;

namespace RestWithNETUdemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO personVO);
        PersonVO FindById(long id);
        List<PersonVO> FindByName(string firstName, string lastName);
        List<PersonVO> FindAll();
        PagedSearchVO<PersonVO> FindWithPagedSearch(string firstName, string sortDirection, int pageSize, int page);
        PersonVO Update(PersonVO personVO);
        PersonVO Disabled(long id);
        void Delete(long id);

    }
}
