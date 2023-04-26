using hmwk53.data;
using hmwk53.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hmwk53.web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPeople()
        {
            PeopleRepo peopleRepo = new(_connectionString);
            return Json(peopleRepo.GetAll());
        }
        [HttpPost]
        public void AddPerson(Person person)
        {
            PeopleRepo peopleRepo = new(_connectionString);
            peopleRepo.Add(person);
        }
        public void DeletePerson(int id)
        {
            PeopleRepo peopleRepo = new(_connectionString);
            peopleRepo.DeletePerson(id);
        }
        public void UpdatePerson(Person person)
        {
            PeopleRepo peopleRepo = new(_connectionString);
            peopleRepo.UpdatePerson(person);
        }
        public Person GetPersonToEdit(int editId)
        {
            PeopleRepo peopleRepo = new(_connectionString);
            return peopleRepo.GetPerson(editId);
        }


    }
}