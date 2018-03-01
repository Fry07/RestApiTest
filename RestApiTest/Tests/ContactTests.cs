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
            var json = GetContactByID("1").Content;

            Assert.AreEqual("john.doe@unknown.com", GetEmail(json));
            Assert.AreEqual("John", GetFirstName(json));
            Assert.AreEqual("Doe", GetLastName(json));
        }

        [Test]
        public void Create()
        {
            string email = "mail@mail.com";
            string first = "ivan";
            string last = "ivanov";

            var status = CreateContactAndReturnID(email, first, last);
            var json = GetContactByID(status).Content;

            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test]
        public void Edit()
        {
            string email = "mail@mail.com.123";
            string first = "ivan123123123";
            string last = "ivanov123123123";
            string id = "12";

            var status = EditContactAndReturnID(id, email, first, last);
            var json = GetContactByID(id).Content;

            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));

        }

        [Test]
        public void Delete()
        {
            string id = "12";
            Assert.AreEqual(HttpStatusCode.OK.ToString(), DeleteContact(id));
            Assert.AreEqual(HttpStatusCode.NotFound, GetContactByID(id).StatusCode);

        }
    }
}
