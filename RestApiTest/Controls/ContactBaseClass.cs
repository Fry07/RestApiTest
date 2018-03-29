using Newtonsoft.Json;
using RestApiTest.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestApiTest.Controls
{
    public class ContactBaseClass
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
            return response;
        }

        public static IRestResponse CreateContact(Contact contact)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint, Method.POST);
            request.AddJsonBody(new {contact.email, contact.firstName, contact.lastName});
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static IRestResponse EditContact(int id, Contact contact)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint + id, Method.PUT);
            request.AddJsonBody(new {contact.email, contact.firstName, contact.lastName});
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static IRestResponse PatchContact(int id, string parameter, string value)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint + id, Method.PATCH);
            string jsonToSend = "{\"" + parameter + "\":\"" + value + "\"}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static IRestResponse DeleteContact(int id)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint + id, Method.DELETE);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static IRestResponse DeleteAllContacts()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint, Method.DELETE);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static IRestResponse FindContact(string parameter, string value)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Properties.Webserver.Default.baseURL);
            var request = new RestRequest(Properties.Webserver.Default.apiVersion + Properties.Webserver.Default.contactsEndpoint + String.Format("?{0}={1}", parameter, value), Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
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
    }
}
