using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StandardPhonesBook.Core.Entities;
using StandardPhonesBook.Core.Repositories;

namespace LabCore.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiDictController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ApiDictController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var persons = _uow.PersonRepository.All();
            if (persons == null)
            {
                return NoContent();
            }
            return Ok(persons);
            
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Person> Get(Guid id)
        {
            var person = _uow.PersonRepository.Get(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpDelete("{id:guid}")]
        public ActionResult<Person> Delete(Guid id)
        {
            var person = _uow.PersonRepository.Get(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            _uow.PersonRepository.Delete(p=>p.Id==id);
            _uow.Commit();
            return Ok(person);
        }

        [HttpPost]
        public ActionResult<Person> Post(Person person)
        {
            if (person.PersonName == null || person.PhoneNumber == null || person.Id == Guid.Empty)
            {
                return BadRequest("Invalid JSON format");
            }

            if (_uow.PersonRepository.Get(p =>
                    p.PersonName == person.PersonName || p.PhoneNumber == person.PhoneNumber || p.Id == person.Id) !=
                null)
            {
                return Conflict("Record exist with ID, phone or name exist");
            }
            _uow.PersonRepository.Insert(person);
            _uow.Commit();
            return Ok(person);
        }

        [HttpPut]
        public ActionResult<Person> Put(Person person)
        {
            if (person.PersonName == null || person.PhoneNumber == null || person.Id == Guid.Empty)
            {
                return BadRequest("Invalid JSON format");
            }

            var updatedPerson = _uow.PersonRepository.Get(p => p.Id == person.Id);
            if (updatedPerson == null)
            {
                return NotFound();
            }

            updatedPerson.PersonName = person.PersonName;
            updatedPerson.PhoneNumber = person.PhoneNumber;

            _uow.Commit();
            return Ok(updatedPerson);
        }

    }
}