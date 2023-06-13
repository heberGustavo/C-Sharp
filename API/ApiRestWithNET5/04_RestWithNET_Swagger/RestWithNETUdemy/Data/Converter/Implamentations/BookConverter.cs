using RestWithNETUdemy.Data.Converter.Contract;
using RestWithNETUdemy.Data.VO;
using RestWithNETUdemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithNETUdemy.Data.Converter.Implamentations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public BookVO Parse(Book origem)
        {
            if (origem == null) return null;
            return new BookVO
            {
                Id = origem.Id,
                Author = origem.Author,
                LaunchDate = origem.LaunchDate,
                Price = origem.Price,
                Title = origem.Title
            };
        }

        public Book Parse(BookVO origem)
        {
            if (origem == null) return null;
            return new Book
            {
                Id = origem.Id,
                Author = origem.Author,
                LaunchDate = origem.LaunchDate,
                Price = origem.Price,
                Title = origem.Title
            };
        }

        public List<BookVO> Parse(List<Book> origem)
        {
            if (origem == null) return null;
            return origem.Select(item => Parse(item)).ToList();
        }

        public List<Book> Parse(List<BookVO> origem)
        {
            if (origem == null) return null;
            return origem.Select(item => Parse(item)).ToList();
        }
    }
}
