using System;
using LabCore.Models;
using Microsoft.AspNetCore.Mvc;
using StandardPhonesBook.Core.Entities;
using StandardPhonesBook.Core.Repositories;

namespace LabCore.Controllers
{
    public class DictController : Controller
    {
        private readonly IUnitOfWork _uow;

        public DictController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        [HttpGet("")]
        public ActionResult Index()
        {
            return View(_uow.PersonRepository.All());
        }

        [HttpGet("[action]")]
        public ActionResult Add()
        {
            return View(new PersonValidationModel());
        }

        public ActionResult Partial()
        {
            return View();
        }

        [HttpPost("[action]")]
        public ActionResult AddSave(PersonValidationModel personValidationModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Add");
            }

            Predicate<Person> predicate = x =>
                x.PersonName == personValidationModel.PersonName || x.PhoneNumber == personValidationModel.PhoneNumber;
            var person = _uow.PersonRepository.Get(predicate);
            if(person != null) return RedirectToAction("Index");

            _uow.PersonRepository.Insert(new Person()
                {
                    Id = Guid.NewGuid(),
                    PersonName = personValidationModel.PersonName,
                    PhoneNumber = personValidationModel.PhoneNumber
                });
                _uow.Commit();

            return RedirectToAction("Index");
        }

        [HttpGet("[action]")]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost("[action]")]
        public ActionResult DeleteSave(string phone)
        {
            Predicate<Person> predicate = x => x.PhoneNumber == phone;
            _uow.PersonRepository.Delete(predicate);
            _uow.Commit();
            return RedirectToAction("Index");

        }

        [HttpGet("[action]")]
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost("[action]")]
        public ActionResult UpdateSave(PersonValidationModel personValidationModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Update");
            }

            Predicate<Person> predicate = x => x.PersonName == personValidationModel.PersonName;
            var person = _uow.PersonRepository.Get(predicate);
            if (person == null) return RedirectToAction("Index");

            person.PhoneNumber = personValidationModel.PhoneNumber;
            _uow.Commit();

            return RedirectToAction("Index");
        }

    }
}