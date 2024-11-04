using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickSurprise.Context.Entities;
using QuickSurprise.Services;

namespace QuickSurprise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ICrudService _crudService;

        public PersonController(ICrudService crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var persons = _crudService.GetAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var person = _crudService.GetById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            _crudService.Add(person);
            return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Person person)
        {
            if (id != person.Id) return BadRequest();

            var existingPerson = _crudService.GetById(id);
            if (existingPerson == null) return NotFound();

            _crudService.Update(person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _crudService.GetById(id);
            if (person == null) return NotFound();

            _crudService.Delete(id);
            return NoContent();
        }
    }
}

