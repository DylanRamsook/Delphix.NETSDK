using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;
using RestSharp.Serialization;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Net;

namespace DelphixLibrary.Authentication
{
    public static class Session
    {
        public static Cookie jSessionId { get; set; }
        public static RestClient delphixClient { get; set; }
        public static void CreateSession(String username,String password,String instanceUrl) {
            try
            {
                dynamic apiVersion = new JObject();
                apiVersion.type = "APIVersion";
                apiVersion.major = 1;
                apiVersion.minor = 4;
                apiVersion.micro = 3;

                dynamic body = new JObject();
                body.type = "APISession";
                body.version = apiVersion;

                body = JsonConvert.SerializeObject(body);
                var client = new RestClient(instanceUrl);
                client.CookieContainer = new System.Net.CookieContainer();
                var request = new RestRequest("resources/json/delphix/session", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(body);
                request.AddHeader("content-header", "application/json");
                var cancellationTokenSource = new CancellationTokenSource();
                //var result =
                //    await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
                var result = client.Post(request);
                Console.WriteLine(body);
                //jSessionId = result.Cookies.First().Value.ToString();
                //jSessionId = result.Cookies.First().Value.ToString() ;
                jSessionId = new Cookie (result.Cookies.First().Name,result.Cookies.First().Value);
                var cookie = new Cookie();
                cookie.Name = result.Cookies.First().Name;
                cookie.Value = result.Cookies.First().Value;
                cookie.Domain = result.Cookies.First().Domain;
                cookie.Path = result.Cookies.First().Path;
                cookie.HttpOnly = result.Cookies.First().HttpOnly;
                cookie.Secure = result.Cookies.First().Secure;
                //client.CookieContainer.Add(cookie);
                Console.WriteLine(jSessionId);

                dynamic loginJson = new JObject();
                loginJson.type = "LoginRequest";
                loginJson.username =  username;
                loginJson.password = password;

                loginJson = JsonConvert.SerializeObject(loginJson);

                var request2 = new RestRequest("resources/json/delphix/login", Method.POST);
                request2.AddCookie(result.Cookies.First().Name, result.Cookies.First().Value);
                request2.AddJsonBody(loginJson);
                request2.AddHeader("content-header", "application/json");
                request2.AddParameter(result.Cookies.First().Name, result.Cookies.First().Value, ParameterType.Cookie);
                client.AddDefaultParameter(result.Cookies.First().Name, result.Cookies.First().Value, ParameterType.Cookie);
                client.CookieContainer.Add(cookie);

                //request2.AddCookie("JSESSIONID",jSessionId);
                var cancellationTokenSource2 = new CancellationTokenSource();
                //var result =
                //    await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
                var result2 = client.Execute(request2);

                //jSessionId = result.Cookies.First().Value.ToString();
                jSessionId = new Cookie(result.Cookies.First().Name, result.Cookies.First().Value);
                client.AddDefaultHeader(result.Cookies.First().Name, result.Cookies.First().Value);
                delphixClient = client;
                Console.WriteLine(result2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
            }
        }

    }
    //static class Version
    //{
    //    public static string type { get; set; }
    //    public static int major { get; set; }
    //    public static int minor { get; set; }
    //    public static int micro { get; set; }
    //}

    //static class SessionType
    //{
    //    public static string type { get; set; }
    //    public static Version version { get; set; }
    //}
}
