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


using DelphixLibrary.Authentication;

namespace DelphixLibrary.Repository
{
    public class RepositoryService
    {
        public List<DelphixRepository> GetRepositories()
        {
            //List<DelphixDatabase> deserializedDbList = new List<DelphixDatabase>();
            //string groups = "";
            var request = new RestRequest("resources/json/delphix/repository", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(body);
            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            try
            {
                var result = Session.delphixClient.Get(request);
                string dbs = result.Content;
                var response = JsonConvert.DeserializeObject<GetRepositoryResponse>(dbs);
                if (response.status.Equals("OK"))
                {
                    var deserializedDbs = response.result;
                    return deserializedDbs;
                }
                else
                {
                    throw new Exception("The status returned from the GetDatabases call was NOT OK");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<DelphixRepository> GetRepositoryByEnvironmentRef(string environmentReference, List<DelphixRepository> sourceConfigs)
        {
            List<DelphixRepository> groupDatabases = new List<DelphixRepository>();
            foreach (DelphixRepository delphixDb in sourceConfigs)
            {
                if (delphixDb.environment.ToString().Equals(environmentReference) && delphixDb.type.Equals("MSSqlInstance"))
                {
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }
    }
}
