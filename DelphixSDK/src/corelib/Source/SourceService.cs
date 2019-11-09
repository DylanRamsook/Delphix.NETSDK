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

namespace DelphixLibrary.Source
{
    public class SourceService
    {
        public List<DelphixSource> GetSources()
        {
            //List<DelphixDatabase> deserializedDbList = new List<DelphixDatabase>();
            //string groups = "";
            var request = new RestRequest("resources/json/delphix/source", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(body);
            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            try
            {
                var result = Session.delphixClient.Get(request);
                string dbs = result.Content;
                var response = JsonConvert.DeserializeObject<GetSourcesResponse>(dbs);
                if (response.status.Equals("OK"))
                {
                    var deserializedDbs = response.result;
                    return deserializedDbs;
                }
                else {
                    throw new Exception("The status returned from the GetDatabases call was NOT OK");
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        public string DisableSource(DelphixSource dbToDelete)
        {

            var request = new RestRequest("resources/json/delphix/source/" + dbToDelete.reference.ToString() + "/disable", Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);

            var result = Session.delphixClient.Post(request);
            string dbs = result.Content;

            var response = JsonConvert.DeserializeObject<ProvisionVdbResponse>(dbs);
            if (response.status.Equals("OK"))
            {
                var deserializedDbs = response.job;
                return deserializedDbs;
            }
            else
            {
                Console.WriteLine("The status returned from the GetDatabases call was NOT OK");
                return "";
            }

        }
        public string EnableSource(DelphixSource dbToDelete)
        {

            //dynamic DeleteParameters = new JObject();
            //DeleteParameters.type = "DeleteParameters";




            // DeleteParameters = JsonConvert.SerializeObject(DeleteParameters);
            var request = new RestRequest("resources/json/delphix/source/" + dbToDelete.reference.ToString() + "/enable", Method.POST);
            request.RequestFormat = DataFormat.Json;
            // request.AddBody(DeleteParameters);

            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);

            var result = Session.delphixClient.Post(request);
            string dbs = result.Content;
            //Console.WriteLine(DeleteParameters);
            var response = JsonConvert.DeserializeObject<ProvisionVdbResponse>(dbs);
            if (response.status.Equals("OK"))
            {
                var deserializedDbs = response.job;
                return deserializedDbs;
            }
            else
            {
                Console.WriteLine("The status returned from the GetDatabases call was NOT OK");
                return "";
            }

        }

        public List<DelphixSource> GetSourceConfigByDbName(string groupReference, List<DelphixSource> dbs)
        {
            List<DelphixSource> groupDatabases = new List<DelphixSource>();
            foreach (DelphixSource delphixDb in dbs)
            {
                if (delphixDb.name.ToString().Equals(groupReference)) {
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }

        public DelphixSource GetSourceByConfig(string config, List<DelphixSource> dbs)
        {
            DelphixSource groupDatabases = new DelphixSource();


            foreach (DelphixSource delphixDb in dbs)
            {
                if (delphixDb.config.ToString().Equals(config))
                {
                    groupDatabases = delphixDb;
                }
            }



            return groupDatabases;

        }
        public DelphixSource GetSourceByReference(string reference, List<DelphixSource> dbs)
        {
            DelphixSource groupDatabases = new DelphixSource();


            foreach (DelphixSource delphixDb in dbs)
            {
                if (delphixDb.reference.ToString().Equals(reference))
                {
                    groupDatabases = delphixDb;
                }
            }



            return groupDatabases;

        }

        public DelphixSource GetSourceByContainer(string config, List<DelphixSource> dbs)
        {
            DelphixSource groupDatabases = new DelphixSource();


            foreach (DelphixSource delphixDb in dbs)
            {
                if (delphixDb.container.ToString().Equals(config))
                {
                    groupDatabases = delphixDb;
                }
            }



            return groupDatabases;

        }

    }
}
