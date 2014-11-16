using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventurousContacts;
using AdventurousContacts.Controllers;
using System.Collections;
using Moq;
using AdventurousContacts.Models.Repositories;
using AdventurousContacts.Models;

namespace AdventurousContacts.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        private Mock<IRepository> _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new Mock<IRepository>();
        }

        [TestMethod]
        public void IndexShouldReturnView()
        {
            
            var expectedContactList = new List<Contact>();

            // Tell mockRepository how it should behave using Setup(), it specifies the exact call the mockobject should expect.
            // The Returns method is used to specify the value that should be returned in response to the expected call.
            _repository.Setup(repo => repo.GetContacts())
                .Returns(expectedContactList);

            var controller = new ContactController(_repository.Object);
            dynamic result = controller.Index();

            Assert.AreSame(expectedContactList, result.Model);
        }

        [TestMethod]
        public void CreateShouldInvokeCreateActionsWhenModelStateIsValid()
        {
            var controller = new ContactController(_repository.Object);
            controller.Create(It.IsAny<Contact>());
            _repository.Verify(obj => obj.Add(It.IsAny<Contact>()), Times.Once());
            _repository.Verify(obj => obj.Save(), Times.Once());
            
            // TODO: Test if the RedirectToAction() is excecuted

        }

        [TestMethod]
        public void CreateShouldReturnCreateViewWhenModelStateIsValidFails()
        {
            var controller = new ContactController(_repository.Object);

            controller.ModelState.AddModelError("notvalid", "testerror");     
            dynamic result = controller.Create(It.IsAny<Contact>());

            Assert.AreSame("Create", result.ViewName);
        }

        [TestMethod]
        public void DeleteShouldReturnConfirmViewIfContactExists()
        {
            int existingContactId = 1;
            var expectedContact = new Contact()
            {
                ContactID = existingContactId,
                FirstName = "firstname",
                LastName = "lastname"
            };

            _repository.Setup(obj => obj.GetContactById(existingContactId))
                .Returns(expectedContact);

            var controller = new ContactController(_repository.Object);
            dynamic result = controller.Delete(existingContactId);

            Assert.AreSame("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFoudViewIfContactDoesNotExist()
        {
            int existingContactId = 1;
            int nonExistingContactId = 2;
            var expectedContact = new Contact()
            {
                ContactID = existingContactId,
                FirstName = "firstname",
                LastName = "lastname",
                EmailAddress = "firstname.lastname@mail.com"
            };

            _repository.Setup(obj => obj.GetContactById(existingContactId))
                .Returns(expectedContact);

            var controller = new ContactController(_repository.Object);
            dynamic result = controller.Delete(nonExistingContactId);

            Assert.AreSame("NotFound", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedShouldInvokeDeleteActions()
        {
            int existingContactId = 1;
            var expectedContact = new Contact()
            {
                ContactID = existingContactId,
                FirstName = "firstname",
                LastName = "lastname",
                EmailAddress = "firstname.lastname@mail.com"
            };

            var controller = new ContactController(_repository.Object);

            _repository.Setup(obj => obj.GetContactById(existingContactId))
                .Returns(expectedContact);

            dynamic result = controller.DeleteConfirmed(existingContactId);

            _repository.Verify(obj => obj.Delete(expectedContact), Times.Once());
            _repository.Verify(obj => obj.Save(), Times.Once());                  
        }

        //[TestMethod]
        //public void DeleteConfirmedShould
    }
}
