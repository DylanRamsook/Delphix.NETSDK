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

namespace DelphixLibrary.SourceConfig
{
    public class SourceConfigService
    {
        public List<DelphixSourceConfig> GetSourceConfigs()
        {
            //List<DelphixDatabase> deserializedDbList = new List<DelphixDatabase>();
            //string groups = "";
            var request = new RestRequest("resources/json/delphix/sourceconfig", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(body);
            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            try
            {
                var result = Session.delphixClient.Get(request);
                string dbs = result.Content;
                var response = JsonConvert.DeserializeObject<GetSourceConfigsResponse>(dbs);
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

        public List<DelphixSourceConfig> GetSourceConfigsByRepository(string repositoryReference, List<DelphixSourceConfig> sourceConfigs)
        {
            List<DelphixSourceConfig> groupDatabases = new List<DelphixSourceConfig>();
            foreach (DelphixSourceConfig delphixDb in sourceConfigs)
            {
                if (delphixDb.repository.ToString().Equals(repositoryReference))
                {
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }
        public List<DelphixSourceConfig> GetSourceConfigsByHost(string host, List<DelphixSourceConfig> sourceConfigs)
        {
            List<DelphixSourceConfig> groupDatabases = new List<DelphixSourceConfig>();
            foreach (DelphixSourceConfig delphixDb in sourceConfigs)
            {
                if (delphixDb.instance.host.ToString().Equals(host))
                {
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }
        public List<DelphixSourceConfig> GetSourceConfigsByReference(string configReference, List<DelphixSourceConfig> sourceConfigs)
        {
            List<DelphixSourceConfig> groupDatabases = new List<DelphixSourceConfig>();
            foreach (DelphixSourceConfig delphixDb in sourceConfigs)
            {
                if (delphixDb.reference.ToString().Equals(configReference))
                {
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }
    }
}
