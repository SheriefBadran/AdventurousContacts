﻿using System;
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
        private ContactController _controller;
        private Contact _expectedContact;

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new Mock<IRepository>();
            _controller = new ContactController(_repository.Object);
            _expectedContact = new Contact()
            {
                ContactID = 0,
                FirstName = "firstname",
                LastName = "lastname",
                EmailAddress = "firstname.lastname@mail.com"
            };
        }

        [TestMethod]
        public void IndexShouldReturnView()
        {
            
            var expectedContactList = new List<Contact>();

            // Tell mockRepository how it should behave using Setup(), it specifies the exact call the mockobject should expect.
            // The Returns method is used to specify the value that should be returned in response to the expected call.
            _repository.Setup(repo => repo.GetContacts())
                .Returns(expectedContactList);

            dynamic result = _controller.Index();

            Assert.AreSame(expectedContactList, result.Model);
        }

        [TestMethod]
        public void CreateShouldInvokeCreateActionsWhenModelStateIsValid()
        {
            _controller.Create(It.IsAny<Contact>());

            _repository.Verify(obj => obj.Add(It.IsAny<Contact>()), Times.Once());
            _repository.Verify(obj => obj.Save(), Times.Once());
        }

        [TestMethod]
        public void CreateShouldRedirectToCreateActionAfterSavingContact()
        {
            var result = _controller.Create(It.IsAny<Contact>()) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["Action"], "Create");
        }

        [TestMethod]
        public void CreateShouldReturnCreateViewWhenModelStateIsValidFails()
        {
            _controller.ModelState.AddModelError("notvalid", "testerror");
            dynamic result = _controller.Create(It.IsAny<Contact>());

            Assert.AreSame("Create", result.ViewName);
        }

        [TestMethod]
        public void DeleteShouldReturnConfirmViewIfContactExists()
        {
            int existingContactId = 1;
            _expectedContact.ContactID = existingContactId;

            _repository.Setup(obj => obj.GetContactById(existingContactId))
                .Returns(_expectedContact);

            // Try delete existing contact
            dynamic result = _controller.Delete(existingContactId);

            Assert.AreSame("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFoudViewIfContactDoesNotExist()
        {
            int existingContactId = 1;
            int nonExistingContactId = 2;
            _expectedContact.ContactID = existingContactId;

            _repository.Setup(obj => obj.GetContactById(existingContactId))
                .Returns(_expectedContact);

            // Try delete nonexisting contact
            dynamic result = _controller.Delete(nonExistingContactId);

            Assert.AreSame("NotFound", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedShouldInvokeDeleteActions()
        {
            int existingContactId = 1;
            _expectedContact.ContactID = existingContactId;

            _repository.Setup(obj => obj.GetContactById(existingContactId))
                .Returns(_expectedContact);

            dynamic result = _controller.DeleteConfirmed(existingContactId);

            _repository.Verify(obj => obj.Delete(_expectedContact), Times.Once());
            _repository.Verify(obj => obj.Save(), Times.Once());     
        }

        [TestMethod]
        public void DeleteConfirmedShouldReturnRedirectToDeletedAction()
        {
            var result = _controller.DeleteConfirmed(1) as RedirectToRouteResult;
            Assert.AreEqual(result.RouteValues["Action"], "Deleted");
        }
    }
}
