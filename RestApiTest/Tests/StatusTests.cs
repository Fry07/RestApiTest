using NUnit.Framework;
using RestApiTest.Controls;
using RestSharp;
using System;
using System.Net;

namespace RestApiTest
{
    [TestFixture]
    public class StatusTests : BaseClass
    {
        [Test]
        public void ResponseReturnedContacts200()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint);

            IRestResponse response = client.Execute(request);            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                string.Format("Status code {0} does not match actually {1}", HttpStatusCode.OK, response.StatusCode));
        }

        [Test]
        public void ResponseReturnedHealthCheck200()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.healthcheckEndpoint);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                string.Format("Status code {0} does not match actually {1}", HttpStatusCode.OK, response.StatusCode));
        }

        [Test]
        public void ResponseReturnedApplication200()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.applicationEndpoint);

            IRestResponse response = client.Execute(request);            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                string.Format("Status code {0} does not match actually {1}", HttpStatusCode.OK, response.StatusCode));
        }
    }
}
