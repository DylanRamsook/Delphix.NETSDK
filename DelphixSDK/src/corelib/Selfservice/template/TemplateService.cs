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
using NLog;

using DelphixLibrary.Authentication;
using DelphixLibrary.Database;

namespace DelphixLibrary.Template
{
    public class TemplateService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
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

        public DelphixResponse CreateTemplate(string environmentName, List<DelphixDatabase> vdbs)
        /*
         * EXAMPLE REQUEST BODY:
         *{
  "name": "CTT42",
  "dataSources": [
    {
      "source": {
        "properties": {},
        "priority": 1,
        "name": "cttdsmeta",
        "type": "JSDataSource"
      },
      "container": "MSSQL_DB_CONTAINER-770",
      "type": "JSDataSourceCreateParameters"
    },
    {
      "source": {
        "properties": {},
        "priority": 1,
        "name": "ctt42dsss",
        "type": "JSDataSource"
      },
      "container": "MSSQL_DB_CONTAINER-757",
      "type": "JSDataSourceCreateParameters"
    }
  ],
  "properties": {},
  "type": "JSDataTemplateCreateParameters"
}

        EXAMPLE ERROR RESPONSE (200 OK RESPONSE) :
        {
    "type": "ErrorResult",
    "status": "ERROR",
    "error": {
        "type": "APIError",
        "details": "The operation could not be completed because a JS_DATA_TEMPLATE with the following fields already exists: name (CTT42).",
        "action": "Resolve the conflict by changing the values of the following fields and try again: name.",
        "id": "exception.executor.object.exists",
        "commandOutput": null
    }
}

        EXAMPLE SUCCESS RESPONSE: 
        {
    "type": "OKResult",
    "status": "OK",
    "result": "JS_DATA_TEMPLATE-8",
    "job": null,
    "action": "ACTION-11675"
}
        */


        {

            CreateDelphixTemplateRequest requestBody = new CreateDelphixTemplateRequest();
            requestBody.dataSources = new List<DataSource>();
            requestBody.type = "JSDataTemplateCreateParameters";
            requestBody.name = environmentName;
            foreach (DelphixDatabase vdb in vdbs)
            {
                DataSource anuddaOne = new DataSource();
                anuddaOne.source = new Source();

                //anuddaOne.source.properties;
                anuddaOne.source.priority = 1;
                anuddaOne.source.name = vdb.name;
                anuddaOne.source.type = "JSDataSource";
                anuddaOne.container = vdb.reference;
                anuddaOne.type = "JSDataSourceCreateParameters";

                requestBody.dataSources.Add(anuddaOne) ;
            }

            

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var jsRequestBody = JsonConvert.SerializeObject(requestBody,settings);
            
            var request = new RestRequest("resources/json/delphix/selfservice/template", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsRequestBody);

            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            try
            {
                var result = Session.delphixClient.Post(request);
                string dbs = result.Content;
                Console.WriteLine(requestBody);
                var response = JsonConvert.DeserializeObject<DelphixResponse>(dbs);
                if (response.status.Equals("OK"))
                {
                    //var deserializedDbs = response.job;
                    logger.Info(response.ToString());
                    return response;
                }
                else
                {
                    var err = JsonConvert.DeserializeObject<DelphixResponseError>(result.Content);
                    //This means there was an error actually creating a job to provision a Vdb.  Check Request Body + if Delphix was reachable. 
                    Console.WriteLine("The status returned from the CreateTemplate call was NOT OK");
                    logger.Error("There was an error creating a Job for the CreateTemplate call.  The response status was: " + response.status + "Request Body:");
                    logger.Info(requestBody.ToString());
                    logger.Error(err.error.details);
                    //return response.status;
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }
    }
}
