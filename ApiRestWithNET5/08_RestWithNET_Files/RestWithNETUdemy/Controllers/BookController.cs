using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithNETUdemy.Business;
using RestWithNETUdemy.Data.VO;
using RestWithNETUdemy.Hypermedia.Filters;
using RestWithNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness _bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll()); 
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var bookVO = _bookBusiness.FindById(id);

            if (bookVO == null)
                return NotFound();

            return Ok(bookVO);
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Create([FromBody] BookVO bookVO)
        {
            if (bookVO == null)
                return BadRequest();

            return Ok(_bookBusiness.Create(bookVO));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Update([FromBody] BookVO bookVO)
        {
            if (bookVO == null)
                return BadRequest();

            return Ok(_bookBusiness.Update(bookVO));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
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
