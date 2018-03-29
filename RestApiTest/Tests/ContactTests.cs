using NUnit.Framework;
using RestApiTest.Controls;
using RestApiTest.Model;
using RestSharp;
using System.Net;

namespace RestApiTest.Tests
{
    [TestFixture]
    public class ContactTestsPositive : ContactBaseClass
    {
        int id;
        IRestResponse response;
        Contact contact;

        [SetUp]
        public void Init()
        {
            Contact contact = new Contact();
            this.contact = contact;

            //create contact and get its JSON by id
            id = GetID(CreateContact(contact).Content);
            response = GetContactByID(id);            
        }

        [TearDown]
        public void Dispose()
        {
            DeleteContact(id);
        }

        [Test, Category("Positive"), Category("Find")]
        public void AssertContactWithIDEquals1()
        {
            contact.email = "john.doe@unknown.com";
            contact.firstName = "John";
            contact.lastName = "Doe";            
            response = GetContactByID(1);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(contact.email, GetEmail(response.Content));
                Assert.AreEqual(contact.firstName, GetFirstName(response.Content));
                Assert.AreEqual(contact.lastName, GetLastName(response.Content));
            });
        }

        [Test, Category("Positive"), Category("Create")]
        public void CreateNewContact()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(contact.email, GetEmail(response.Content));
                Assert.AreEqual(contact.firstName, GetFirstName(response.Content));
                Assert.AreEqual(contact.lastName, GetLastName(response.Content));
            });
        }

        [Test, Category("Positive"), Category("Edit")]
        public void EditContact()
        {
            contact.email += "_edit";
            contact.firstName += "_edit";
            contact.lastName += "_edit";
            response = EditContact(id, contact);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(contact.email, GetEmail(response.Content));
                Assert.AreEqual(contact.firstName, GetFirstName(response.Content));
                Assert.AreEqual(contact.lastName, GetLastName(response.Content));
            });
        }
        
        [Test, Category("Positive"), Category("Edit")]
        public void ChangeEmail()
        {            
            contact.email = "very-new@mail.com";
            response = PatchContact(id, "email", contact.email);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(contact.email, GetEmail(response.Content));
            });
        }

        [Test, Category("Positive"), Category("Edit")]
        public void ChangeFirstName()
        {
            contact.firstName = "Bill";
            response = PatchContact(id, "firstName", contact.firstName);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(contact.firstName, GetFirstName(response.Content));
            });
        }

        [Test, Category("Positive"), Category("Edit")]
        public void ChangeLastName()
        {
            contact.lastName = "Robson";
            response = PatchContact(id, "lastName", contact.lastName);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(contact.lastName, GetLastName(response.Content));
            });
        }

        [Test, Category("Positive"), Category("Delete")]
        public void DeleteOneContact()
        {
            var tmp = id;
            response = CreateContact(contact);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            id = GetLastID(GetAllContacts().Content);
            Assert.AreEqual(HttpStatusCode.NotFound, GetContactByID(GetID(DeleteContact(id).Content)).StatusCode); //delete contact, then try to find this contact and get its status by id
            id = tmp;
        }

        [Test, Category("Positive"), Category("Find")]
        public void FindContactByEmail()
        {
            contact.firstName = "John";
            response = FindContact("email", "john.doe@unknown.com");

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(contact.firstName, GetFirstName(response.Content));
            });
        }

        [Test, Category("Positive"), Category("Find")]
        public void FindContactByFirstName()
        {
            contact.lastName = "Doe";
            response = FindContact("firstName", "John");

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(contact.lastName, GetLastName(response.Content));
            });
        }

        [Test, Category("Positive"), Category("Find")]
        public void FindContactByLastName()
        {
            contact.email = "john.doe@unknown.com";
            response = FindContact("lastName", "Doe");

            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(contact.email, GetEmail(response.Content));
            });
        }
    }

    [TestFixture]
    public class ContactTestsNegative : ContactBaseClass
    {
        int id;
        IRestResponse response;       

        [Test, Category("Negative"), Category("Create")]
        public void CreateNewContactWithoutEmail()
        {
            Contact contact = new Contact();
            contact.email = null;
            response = CreateContact(contact);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test, Category("Negative"), Category("Create")]
        public void CreateNewContactWithoutFirstName()
        {
            Contact contact = new Contact();
            contact.firstName = null;
            response = CreateContact(contact);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test, Category("Negative"), Category("Create")]
        public void CreateNewContactWithoutLastName()
        {
            Contact contact = new Contact();
            contact.lastName = null;
            response = CreateContact(contact);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test, Category("Negative"), Category("Edit")]
        public void EditContactWithoutFirstName()
        {
            Contact contact = new Contact();
            id = GetID(CreateContact(contact).Content);
            
            contact.email += "_edit";
            contact.lastName += "_edit";
            contact.firstName = null;

            response = EditContact(id, contact);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test, Category("Negative"), Category("Edit")]
        public void EditContactWithoutLastName()
        {
            Contact contact = new Contact();
            id = GetID(CreateContact(contact).Content);
            
            contact.email += "_edit";
            contact.firstName += "_edit";
            contact.lastName = null;

            response = EditContact(id, contact);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test, Category("Negative"), Category("Edit")]
        public void EditContactWithoutEmail()
        {
            Contact contact = new Contact();

            id = GetID(CreateContact(contact).Content);
            
            contact.email = null;
            contact.firstName += "_edit";
            contact.lastName += "_edit";

            response = EditContact(id, contact);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test, Category("Negative"), Category("Delete")]
        public void ClearAllContacts()
        {
            Assert.AreEqual(HttpStatusCode.MethodNotAllowed, DeleteAllContacts().StatusCode);
        }

        [Test, Category("Negative"), Category("Find")]
        public void FindInexistingContact()
        {
            response = FindContact("email", "inexisting@mail.com.ua");
            Assert.AreEqual(0, GetData(response.Content).Count);
        }
    }
}
