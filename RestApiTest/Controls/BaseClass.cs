using Newtonsoft.Json;
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

        public static IRestResponse GetContactByID (string id)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/" + id, Method.GET);
            IRestResponse<RootObject> responseCommentModel = client.Execute<RootObject>(request);

            return responseCommentModel;
        }

        public static string GetAllContacts()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/", Method.GET);
            IRestResponse<RootObject> responseCommentModel = client.Execute<RootObject>(request);

            return responseCommentModel.Content;
        }

        public static string CreateContactAndReturnID(string email, string firstName, string lastName)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/", Method.POST);

            string jsonToSend = "{ \"email\":\""+ email +"\",\"firstName\":\"" + firstName + "\",\"lastName\":\"" + lastName+ "\"}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var json = response.Content;
                RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
                string id = root.data[0].id.ToString();
                return id;
            }                
            else
                return null;

        }

        public static string EditContactAndReturnID(string id, string email, string firstName, string lastName)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/" + id, Method.PUT);

            string jsonToSend = "{ \"email\":\"" + email + "\",\"firstName\":\"" + firstName + "\",\"lastName\":\"" + lastName + "\"}";
            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var json = response.Content;
                RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
                return id;
            }
            else
                return null;

        }

        public static string DeleteContact(string id)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(baseUrl);

            var request = new RestRequest("/api/v1/contacts/" + id, Method.DELETE);
            IRestResponse response = client.Execute(request);
            return response.StatusCode.ToString();
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
