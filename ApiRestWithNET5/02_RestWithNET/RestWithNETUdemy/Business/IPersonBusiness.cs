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
        void Delete(long id);
    }
}
