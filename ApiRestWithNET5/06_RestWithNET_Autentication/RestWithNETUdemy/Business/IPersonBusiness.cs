using RestWithNETUdemy.Data.VO;
using System.Collections.Generic;

namespace RestWithNETUdemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO personVO);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO personVO);
        PersonVO Disabled(long id);
        void Delete(long id);
    }
}
