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

namespace DelphixLibrary.Timeflow
{
    public class TimeflowService
    {
        public List<DelphixTimeFlow> GetTimeflows()
        {
            //List<DelphixDatabase> deserializedDbList = new List<DelphixDatabase>();
            //string groups = "";
            var request = new RestRequest("resources/json/delphix/timeflow", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(body);
            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            try
            {
                var result = Session.delphixClient.Get(request);
                string dbs = result.Content;
                var response = JsonConvert.DeserializeObject<GetTimeFlowsResponse>(dbs);
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

        public List<DelphixTimeFlow> GetTimeflowByDbRef(string environmentReference, List<DelphixTimeFlow> sourceConfigs)
        {
            List<DelphixTimeFlow> groupDatabases = new List<DelphixTimeFlow>();
            foreach (DelphixTimeFlow delphixDb in sourceConfigs)
            {
                if (delphixDb.container.ToString().Equals(environmentReference))
                {
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }
    }
}
