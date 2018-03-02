using NUnit.Framework;
using RestApiTest.Controls;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest
{
    [TestFixture]
    public class StatusTests : BaseClass
    {
        [Test]
        public void ResponseReturnedContacts200()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);
            var request = new RestRequest("/api/v1/contacts");

            IRestResponse response = client.Execute(request);            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                string.Format("Status code {0} does not match actually {1}", HttpStatusCode.OK, response.StatusCode));
        }

        [Test]
        public void ResponseReturnedHealthCheck200()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);
            var request = new RestRequest("healthcheck", Method.GET);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                string.Format("Status code {0} does not match actually {1}", HttpStatusCode.OK, response.StatusCode));
        }

        [Test]
        public void ResponseReturnedDescription200()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);
            var request = new RestRequest("application.wadl");

            IRestResponse response = client.Execute(request);            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                string.Format("Status code {0} does not match actually {1}", HttpStatusCode.OK, response.StatusCode));
        }
    }
}
