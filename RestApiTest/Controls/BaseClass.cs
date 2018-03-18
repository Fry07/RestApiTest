using Newtonsoft.Json;
using RestApiTest.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RestApiTest.Controls
{
    public class BaseClass
    {
        public static IRestResponse GetContactByID (int id)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);

            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint + id, Method.GET);
            IRestResponse response = client.Execute(request);

            return response;
        }

        public static IRestResponse GetAllContacts()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);

            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint, Method.GET);
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.OK) ? response : null;
        }

        public static string CreateContact(string email, string firstName, string lastName)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);

            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint, Method.POST);
            string jsonToSend = "{ \"email\":\""+ email +"\",\"firstName\":\"" + firstName + "\",\"lastName\":\"" + lastName+ "\"}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.Created) ? response.Content : null;
        }

        public static string EditContact(int id, string email, string firstName, string lastName)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);

            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint + id, Method.PUT);
            string jsonToSend = "{\"email\":\"" + email + "\",\"firstName\":\"" + firstName + "\",\"lastName\":\"" + lastName + "\"}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.OK) ? response.Content : null;
        }

        public static string PatchContact(int id, string parameter, string value)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);

            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint + id, Method.PATCH);
            string jsonToSend = "{\"" + parameter + "\":\"" + value + "\"}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.OK) ? response.Content : null;
        }

        public static string DeleteContact(int id)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);

            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint + id, Method.DELETE);
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.OK) ? response.Content : null;
        }

        public static IRestResponse DeleteContact()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);

            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint, Method.DELETE);
            IRestResponse response = client.Execute(request);

            return response;
        }

        public static string FindContactByEmail(string email)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);

            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint + "?email=" + email, Method.GET);
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.OK) ? response.Content : null;
        }

        public static int GetID(string json)
        {
            RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
            return root.data[0].id;
        }

        public static string GetEmail(string json)
        {
            RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
            return root.data[0].info.email;
        }

        public static string GetFirstName(string json)
        {
            RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
            return root.data[0].info.firstName;
        }

        public static string GetLastName(string json)
        {
            RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
            return root.data[0].info.lastName;
        }

        public static int GetLastID(string json)
        {
            RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
            return root.data.Last().id;
        }

        public static List<Datum> GetData(string json)
        {
            RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
            return root.data;
        }

        public static string GetRandomFirstName()
        {
            return Faker.Name.First();
        }

        public static string GetRandomLastName()
        {
            return Faker.Name.Last();
        }

        public static string GenerateEmail(string firstName, string lastName)
        {
            return String.Format("{0}.{1}@mail.com", firstName, lastName).ToLower();             
        }
    }
}
