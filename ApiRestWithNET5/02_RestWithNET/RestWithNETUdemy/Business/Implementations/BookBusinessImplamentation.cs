using RestWithNETUdemy.Data.Converter.Implamentations;
using RestWithNETUdemy.Data.VO;
using RestWithNETUdemy.Model;
using RestWithNETUdemy.Repository;
using System.Collections.Generic;

namespace RestWithNETUdemy.Business.Implementations
{
    public class BookBusinessImplamentation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplamentation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Create(BookVO book)
        {
            var entity = _converter.Parse(book);
            entity = _repository.Create(entity);
            return _converter.Parse(entity);
        }

        public BookVO Update(BookVO book)
        {
            var entity = _converter.Parse(book);
            entity = _repository.Update(entity);
            return _converter.Parse(entity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
