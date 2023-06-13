using RestWithNETUdemy.Data.VO;
using System.Collections.Generic;

namespace RestWithNETUdemy.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO bookVO);
        BookVO Update(BookVO bookVO);
        void Delete(long id);
        BookVO FindById(long id);
        List<BookVO> FindAll();
    }
}
