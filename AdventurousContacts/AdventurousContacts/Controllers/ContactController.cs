using AdventurousContacts.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventurousContacts.Controllers
{
    public class ContactController : Controller
    {

        IRepository _repository;

        //public ContactController()
        //    : this(new Repository())
        //{
        //}

        public ContactController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View("Index", _repository.GetContacts());
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }
    }
}