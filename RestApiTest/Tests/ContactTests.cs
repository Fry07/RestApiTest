using Newtonsoft.Json;
using NUnit.Framework;
using RestApiTest.Controls;
using RestApiTest.Model;
using System.Net;

namespace RestApiTest.Tests
{
    [TestFixture]
    public class ContactTests : BaseClass
    {
        string email;
        string first;
        string last;
        int id;
        string json;

        [SetUp]
        public void Init()
        {
            email = "mail@mail.com";
            first = "ivan";
            last = "ivanov";

            json = GetContactByID(GetID(CreateContact(email, first, last))).Content;
            id = GetLastID(GetAllContacts().Content);
        }

        [TearDown]
        public void Dispose()
        {
            DeleteContact(id);
        }

        [Test]
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

        [Test]
        public void CreateNewContact()
        {
            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test]
        public void EditContact()
        {
            email = "mail@mail.com.123";
            first = "ivan123123123";
            last = "ivanov123123123";
            
            var json = GetContactByID(GetID(EditContact(id, email, first, last).ToString())).Content;

            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test]
        public void DeleteContact()
        {
            var tmp = id;
            json = GetContactByID(GetID(CreateContact(email, first, last))).Content;
            id = GetLastID(GetAllContacts().Content);
            Assert.AreEqual(HttpStatusCode.NotFound, GetContactByID(GetID(DeleteContact(id))).StatusCode);
            id = tmp;
        }

        [Test]
        public void ChangeEmail()
        {            
            email = "very-new@mail.com";

            json = GetContactByID(GetID(PatchContact(id, "email", email))).Content;

            Assert.AreEqual(email, GetEmail(json));
        }

        [Test]
        public void ChangeFirstName()
        {
            first = "Bill";

            json = GetContactByID(GetID(PatchContact(id, "firstName", first))).Content;

            Assert.AreEqual(first, GetFirstName(json));
        }

        [Test]
        public void ChangeLastName()
        {
            last = "Robson";

            json = GetContactByID(GetID(PatchContact(id, "lastName", last))).Content;

            Assert.AreEqual(last, GetLastName(json));
        }

        [Test]
        public void Find()
        {
            last = "Doe";

            var json = GetContactByID(GetID(FindContactByEmail("john.doe@unknown.com"))).Content;

            Assert.AreEqual(last, GetLastName(json));
        }

    }
}
