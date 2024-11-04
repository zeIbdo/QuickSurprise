using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickSurpriseRemake.Dtos;
using QuickSurpriseRemake.Models;
using QuickSurpriseRemake.Services;

namespace QuickSurpriseRemake.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IChangeService<Person> _changeService;

        public PersonController(IChangeService<Person> changeService)
        {
            _changeService = changeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var persons = _changeService.GetAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var person = _changeService.GetById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            _changeService.Create(person);
            return Ok(person);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, PersonUpdateDto dto)
        {

            var existingPerson = _changeService.GetById(id);
            if (existingPerson == null) return NotFound();
            existingPerson.FirstName = dto.FirstName;
            existingPerson.LastName = dto.LastName;
            existingPerson.Age = dto.Age;
            _changeService.Update(existingPerson);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _changeService.GetById(id);
            if (person == null) return NotFound();

            _changeService.Delete(id);
            return NoContent();
        }
    }
}
