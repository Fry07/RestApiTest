using NUnit.Framework;
using RestApiTest.Controls;
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
            first = GetRandomFirstName();
            last = GetRandomLastName();
            email = GenerateEmail(first, last);

            //create contact and get its JSON by id
            id = GetID(CreateContact(email, first, last));
            json = GetContactByID(id).Content;
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
            email += "_edit";
            first += "_edit";
            last += "_edit";
            
            json = EditContact(id, email, first, last);

            Assert.AreEqual(email, GetEmail(json));
            Assert.AreEqual(first, GetFirstName(json));
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test]
        public void DeleteOneContact()
        {
            var tmp = id;
            json = CreateContact(email, first, last);
            id = GetLastID(GetAllContacts().Content);
            Assert.AreEqual(HttpStatusCode.NotFound, GetContactByID(GetID(DeleteContact(id))).StatusCode); //delete contact, then try to find this contact and get its status by id
            id = tmp;
        }

        [Test]
        public void DeleteAllContacts()
        {
            Assert.AreEqual(HttpStatusCode.MethodNotAllowed, DeleteContact().StatusCode);
        }

        [Test]
        public void ChangeEmail()
        {            
            email = "very-new@mail.com";
            json = PatchContact(id, "email", email);
            Assert.AreEqual(email, GetEmail(json));
        }

        [Test]
        public void ChangeFirstName()
        {
            first = "Bill";
            json = PatchContact(id, "firstName", first);
            Assert.AreEqual(first, GetFirstName(json));
        }

        [Test]
        public void ChangeLastName()
        {
            last = "Robson";
            json = PatchContact(id, "lastName", last);
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test]
        public void Find()
        {
            last = "Doe";
            json = FindContactByEmail("john.doe@unknown.com");
            Assert.AreEqual(last, GetLastName(json));
        }

        [Test]
        public void FindNegative()
        {
            json = FindContactByEmail("inexisting@mail.com");
            Assert.AreEqual(0, GetData(json).Count);
        }
    }
}
