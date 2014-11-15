using AdventurousContacts.Models;
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

        public ContactController(IRepository repository)
        {
            _repository = repository;
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View("Index", _repository.GetContacts());
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Add(contact);
                    _repository.Save();
                    TempData.Add("contact", contact);
                    return RedirectToAction("Create");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "An error accured when saving contact.");               
                }
            }

            return View("Create", contact);
        }

        public ActionResult Update()
        {
            return View();
        }
    }
}