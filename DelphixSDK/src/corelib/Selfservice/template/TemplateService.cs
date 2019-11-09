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

namespace DelphixLibrary.Template
{
    class TemplateService
    {
        public List<DelphixTemplate> GetTemplates()
        {
            var request = new RestRequest("resources/json/delphix/selfservice/template", Method.GET);

            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            try
            {
                var result = Session.delphixClient.Get(request);
                string dbs = result.Content;
                var response = JsonConvert.DeserializeObject<GetTemplatesResponse>(dbs);
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

        //public string CreateTemplate()
        //{
        //    string dbName = sourceDb.name.Substring(sourceDb.name.LastIndexOf('-') + 1).Trim();

        //    dynamic container = new JObject();
        //    container.name = "TARGET" + dbName + "12345"; //Fix this
        //    container.type = "MSSqlDatabaseContainer";
        //    container.group = "GROUP-7";

        //    dynamic source = new JObject();
        //    source.type = "MSSqlVirtualSource";
        //    source.config = sourceSourceConfig.reference.ToString();
        //    source.allowAutoVDBRestartOnHostReboot = true;

        //    dynamic sourceConfig = new JObject();
        //    sourceConfig.type = "MSSqlSIConfig";
        //    sourceConfig.databaseName = dbName;  //Fix this
        //    sourceConfig.repository = destinationRepo.reference.ToString();

        //    dynamic timeflowPointParameters = new JObject();
        //    timeflowPointParameters.type = "TimeflowPointSemantic";
        //    timeflowPointParameters.location = "LATEST_SNAPSHOT";
        //    timeflowPointParameters.container = sourceDbRef;
        //    //timeflowPointParameters.timeflow = snapshot.reference;

        //    dynamic ProvisionParameters = new JObject();
        //    ProvisionParameters.container = container;
        //    ProvisionParameters.source = source;
        //    ProvisionParameters.sourceConfig = sourceConfig;
        //    ProvisionParameters.timeflowPointParameters = timeflowPointParameters;
        //    ProvisionParameters.type = "MSSqlProvisionParameters";




        //    ProvisionParameters = JsonConvert.SerializeObject(ProvisionParameters);
        //    var request = new RestRequest("resources/json/delphix/database/provision", Method.POST);
        //    request.RequestFormat = DataFormat.Json;
        //    request.AddBody(ProvisionParameters);

        //    request.AddHeader("content-header", "application/json");
        //    request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
        //    try
        //    {
        //        var result = Session.delphixClient.Post(request);
        //        string dbs = result.Content;
        //        Console.WriteLine(ProvisionParameters);
        //        var response = JsonConvert.DeserializeObject<ProvisionVdbResponse>(dbs);
        //        if (response.status.Equals("OK"))
        //        {
        //            var deserializedDbs = response.job;
        //            return deserializedDbs;
        //        }
        //        else
        //        {
        //            Console.WriteLine("The status returned from the GetDatabases call was NOT OK");
        //            return "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
