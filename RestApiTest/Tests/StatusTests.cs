using NUnit.Framework;
using RestApiTest.Controls;
using RestSharp;
using System;
using System.Net;

namespace RestApiTest.Tests
{
    [TestFixture]
    public class StatusTests : BaseClass
    {
        [Test, Category("Endpoint")]
        public void ResponseReturnedContacts200()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint);

            IRestResponse response = client.Execute(request);            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Category("Endpoint")]
        public void ResponseReturnedHealthCheck200()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.healthcheckEndpoint);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Category("Endpoint")]
        public void ResponseReturnedApplication200()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.applicationEndpoint);

            IRestResponse response = client.Execute(request);            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Category("Endpoint")]
        public void ContactsAllowedMethods()
        {            
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint, Method.OPTIONS);

            string allowedMethods = "HEAD,POST,GET,OPTIONS";
            IRestResponse response = client.Execute(request);            
            Assert.AreEqual(allowedMethods, response.Headers[0].Value);
        }

        [Test, Category("Endpoint")]
        public void ContactWithIdAllowedMethods()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint + "1", Method.OPTIONS);

            string allowedMethods = "HEAD,DELETE,GET,OPTIONS,PUT,PATCH";
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(allowedMethods, response.Headers[0].Value);
        }

        [Test, Category("Endpoint")]
        public void ApplicationAllowedMethods()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.applicationEndpoint, Method.OPTIONS);

            string allowedMethods = "HEAD,GET,OPTIONS";
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(allowedMethods, response.Headers[0].Value);
        }
    }
}
