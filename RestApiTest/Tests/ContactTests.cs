using NUnit.Framework;
using RestApiTest.Controls;
using System;
using System.Net;

namespace RestApiTest.Tests
{
    [TestFixture]
    public class ContactTestsPositive : BaseClass
    {
        string email;
        string first;
        string last;
        int id;
        string json;

        [SetUp]
        public void Init()
        {            
            first = GetRandomFirstName();
            last = GetRandomLastName();
            email = GenerateEmail(first, last);

            //create contact and get its JSON by id
            id = GetID(CreateContact(email, first, last).Content);
            json = GetContactByID(id).Content;
        }

        [TearDown]
        public void Dispose()
        {
            DeleteContact(id);
        }

        [Test, Category("Positive")]
        public void AssertContactWithIDEquals1()
        {
            email = "john.doe@unknown.com";
            first = "John";
            last = "Doe";

            json = GetContactByID(1).Content;

            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test, Category("Positive"), Category("Create")]
        public void CreateNewContact()
        {
            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test, Category("Positive"), Category("Edit")]
        public void EditContact()
        {
            email += "_edit";
            first += "_edit";
            last += "_edit";
            
            json = EditContact(id, email, first, last).Content;

            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));
        }
        
        [Test, Category("Positive"), Category("Edit")]
        public void ChangeEmail()
        {            
            email = "very-new@mail.com";
            json = PatchContact(id, "email", email);
            Assert.AreEqual(email, GetEmail(json));
        }

        [Test, Category("Positive"), Category("Edit")]
        public void ChangeFirstName()
        {
            first = "Bill";
            json = PatchContact(id, "firstName", first);
            Assert.AreEqual(first, GetFirstName(json));
        }

        [Test, Category("Positive"), Category("Edit")]
        public void ChangeLastName()
        {
            last = "Robson";
            json = PatchContact(id, "lastName", last);
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test, Category("Positive"), Category("Delete")]
        public void DeleteOneContact()
        {
            var tmp = id;
            json = CreateContact(email, first, last).Content;
            id = GetLastID(GetAllContacts().Content);
            Assert.AreEqual(HttpStatusCode.NotFound, GetContactByID(GetID(DeleteContact(id))).StatusCode); //delete contact, then try to find this contact and get its status by id
            id = tmp;
        }

        [Test, Category("Positive"), Category("Find")]
        public void FindContactByEmail()
        {
            first = "John";
            json = FindContact("email", "john.doe@unknown.com");
            Assert.AreEqual(first, GetFirstName(json));
        }

        [Test, Category("Positive"), Category("Find")]
        public void FindContactByFirstName()
        {
            last = "Doe";
            json = FindContact("firstName", "John");
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test, Category("Positive"), Category("Find")]
        public void FindContactByLastName()
        {
            email = "john.doe@unknown.com";
            json = FindContact("lastName", "Doe");
            Assert.AreEqual(email, GetEmail(json));
        }
    }

    [TestFixture]
    public class ContactTestsNegative : BaseClass
    {
        string email;
        string first;
        string last;
        int id;
        string json;

        [Test, Category("Negative"), Category("Create")]
        public void CreateNewContactWithoutEmail()
        {
            first = GetRandomFirstName();
            last = GetRandomLastName();
            email = null;

            var respone = CreateContact(email, first, last);
            Assert.AreEqual(HttpStatusCode.BadRequest, respone.StatusCode);
        }

        [Test, Category("Negative"), Category("Create")]
        public void CreateNewContactWithoutFirstName()
        {
            first = null;
            last = GetRandomLastName();
            email = GenerateEmail(first, last); //it is also an inappropriate email format

            var respone = CreateContact(email, first, last);
            Assert.AreEqual(HttpStatusCode.BadRequest, respone.StatusCode);
        }

        [Test, Category("Negative"), Category("Create")]
        public void CreateNewContactWithoutLastName()
        {
            first = GetRandomFirstName();
            last = null;
            email = GenerateEmail(first, last); //it is also an inappropriate email format

            var respone = CreateContact(email, first, last);
            Assert.AreEqual(HttpStatusCode.BadRequest, respone.StatusCode);
        }

        [Test, Category("Negative"), Category("Edit")]
        public void EditContactWithoutFirstName()
        {
            first = GetRandomFirstName();
            last = GetRandomLastName();
            email = GenerateEmail(first, last);
            
            id = GetID(CreateContact(email, first, last).Content);
            json = GetContactByID(id).Content;

            email += "_edit";
            first = null;
            last += "_edit";

            var respone = EditContact(id, email, first, last);
            Assert.AreEqual(HttpStatusCode.BadRequest, respone.StatusCode);
        }

        [Test, Category("Negative"), Category("Edit")]
        public void EditContactWithoutLastName()
        {
            first = GetRandomFirstName();
            last = GetRandomLastName();
            email = GenerateEmail(first, last);

            id = GetID(CreateContact(email, first, last).Content);
            json = GetContactByID(id).Content;

            email += "_edit";
            first += "_edit";
            last = null;

            var respone = EditContact(id, email, first, last);
            Assert.AreEqual(HttpStatusCode.BadRequest, respone.StatusCode);
        }

        [Test, Category("Negative"), Category("Edit")]
        public void EditContactWithoutEmail()
        {
            first = GetRandomFirstName();
            last = GetRandomLastName();
            email = GenerateEmail(first, last);

            id = GetID(CreateContact(email, first, last).Content);
            json = GetContactByID(id).Content;

            email = null;
            first += "_edit";
            last += "_edit";

            var respone = EditContact(id, email, first, last);
            Assert.AreEqual(HttpStatusCode.BadRequest, respone.StatusCode);
        }

        [Test, Category("Negative"), Category("Delete")]
        public void ClearAllContacts()
        {
            Assert.AreEqual(HttpStatusCode.MethodNotAllowed, DeleteAllContacts().StatusCode);
        }

        [Test, Category("Negative"), Category("Find")]
        public void FindInexistingContact()
        {
            json = FindContact("email", "inexisting@mail.com.ua");
            Assert.AreEqual(0, GetData(json).Count);
        }
    }
}
