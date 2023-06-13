using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithNETUdemy.Business;
using RestWithNETUdemy.Data.VO;
using RestWithNETUdemy.Hypermedia.Filters;

namespace RestWithNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personService)
        {
            _logger = logger;
            _personBusiness = personService;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var personVO = _personBusiness.FindById(id);
            
            if (personVO == null) 
                return NotFound();

            return Ok(personVO);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO personVO)
        {
            if (personVO == null)
                return BadRequest();

            return Ok(_personBusiness.Create(personVO));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO personVO)
        {
            if (personVO == null)
                return BadRequest();

            return Ok(_personBusiness.Update(personVO));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent(); 
        }
    }
}
