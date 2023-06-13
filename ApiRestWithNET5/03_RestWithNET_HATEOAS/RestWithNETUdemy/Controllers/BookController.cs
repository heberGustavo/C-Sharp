using Microsoft.AspNetCore.Mvc;
using RestWithNETUdemy.Business;
using RestWithNETUdemy.Data.VO;
using RestWithNETUdemy.Hypermedia.Filters;
using RestWithNETUdemy.Model;

namespace RestWithNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness _bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll()); 
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var bookVO = _bookBusiness.FindById(id);

            if (bookVO == null)
                return NotFound();

            return Ok(bookVO);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Create([FromBody] BookVO bookVO)
        {
            if (bookVO == null)
                return BadRequest();

            return Ok(_bookBusiness.Create(bookVO));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Update([FromBody] BookVO bookVO)
        {
            if (bookVO == null)
                return BadRequest();

            return Ok(_bookBusiness.Update(bookVO));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var bookVO = _bookBusiness.FindById(id);

            if (bookVO == null)
                return NotFound();

            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
