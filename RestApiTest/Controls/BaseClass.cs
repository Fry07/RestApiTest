﻿using Newtonsoft.Json;
using RestApiTest.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.Controls
{
    public class BaseClass
    {
        public static string baseUrl = "http://159.89.100.130:8182/";

        public static IRestResponse GetContactByID (int id)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/" + id, Method.GET);
            IRestResponse response = client.Execute(request);

            return response;
        }

        public static IRestResponse GetAllContacts()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/", Method.GET);
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.OK) ? response : null;
        }

        public static string CreateContact(string email, string firstName, string lastName)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/", Method.POST);
            string jsonToSend = "{ \"email\":\""+ email +"\",\"firstName\":\"" + firstName + "\",\"lastName\":\"" + lastName+ "\"}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.Created) ? response.Content : null;
        }

        public static string EditContact(int id, string email, string firstName, string lastName)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/" + id, Method.PUT);
            string jsonToSend = "{\"email\":\"" + email + "\",\"firstName\":\"" + firstName + "\",\"lastName\":\"" + lastName + "\"}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.OK) ? response.Content : null;
        }

        public static string PatchContact(int id, string parameter, string value)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/" + id, Method.PATCH);
            string jsonToSend = "{\"" + parameter + "\":\"" + value + "\"}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.OK) ? response.Content : null;
        }

        public static string DeleteContact(int id)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/" + id, Method.DELETE);
            IRestResponse response = client.Execute(request);

            return (response.StatusCode == HttpStatusCode.OK) ? response.Content : null;
        }

        public static string FindContactByEmail(string email)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/?email=" + email, Method.GET);
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
    }
}
