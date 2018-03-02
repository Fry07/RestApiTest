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
        [Test]
        public void AssertContactWithIDEquals1()
        {
            string email = "john.doe@unknown.com";
            string first = "John";
            string last = "Doe";

            var json = GetContactByID(1).Content;

            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test]
        public void CreateNewContact()
        {
            string email = "mail@mail.com";
            string first = "ivan";
            string last = "ivanov";
            
            var json = GetContactByID(GetID(CreateContact(email, first, last))).Content;

            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test]
        public void EditContact()
        {
            string email = "mail@mail.com.123";
            string first = "ivan123123123";
            string last = "ivanov123123123";
            int id = 11;
            
            var json = GetContactByID(GetID(EditContact(id, email, first, last).ToString())).Content;

            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test]
        public void DeleteContact()
        {
            int id = 24;

            Assert.AreEqual(HttpStatusCode.NotFound, GetContactByID(GetID(DeleteContact(id))).StatusCode);
        }

        [Test]
        public void ChangeEmail()
        {            
            string email = "very-new@mail.com";
            int id = 16;

            var json = GetContactByID(GetID(PatchContact(id, "email", email))).Content;

            Assert.AreEqual(email, GetEmail(json));
        }

        [Test]
        public void ChangeFirstName()
        {
            string firstName = "Bill";
            int id = 16;

            var json = GetContactByID(GetID(PatchContact(id, "firstName", firstName))).Content;

            Assert.AreEqual(firstName, GetFirstName(json));
        }

        [Test]
        public void ChangeLastName()
        {
            string lastName = "Robson";
            int id = 16;

            var json = GetContactByID(GetID(PatchContact(id, "lastName", lastName))).Content;

            Assert.AreEqual(lastName, GetLastName(json));
        }

        [Test]
        public void Find()
        {
            string lastName = "Robson";

            var json = GetContactByID(GetID(FindContactByEmail("very-new@mail.com"))).Content;

            Assert.AreEqual(lastName, GetLastName(json));
        }
    }
}
