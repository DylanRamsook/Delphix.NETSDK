using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using Newtonsoft.Json;
using NLog;
using RestSharp.Serialization;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Net;

using DelphixLibrary;
using DelphixLibrary.Authentication;
using DelphixLibrary.Environment;
using DelphixLibrary.Repository;
using DelphixLibrary.SourceConfig;
using DelphixLibrary.Timeflow;
using DelphixLibrary.Job;


namespace DelphixLibrary.Database
{
    public class DatabaseService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #region API Calls
        public List<DelphixDatabase> GetDatabases()
        {

            //List<DelphixDatabase> deserializedDbList = new List<DelphixDatabase>();
            //string groups = "";
            var request = new RestRequest("resources/json/delphix/database", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(body);
            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            logger.Info(request.ToString());
            try
            {
                var result = Session.delphixClient.Get(request);
                string dbs = result.Content;
                var response = JsonConvert.DeserializeObject<GetVdbsResponse>(dbs);
                if (response.status.Equals("OK"))
                {
                    var deserializedDbs = response.result;
                    logger.Info(deserializedDbs.ToString());
                    return deserializedDbs;
                }
                else
                {
                    throw new Exception("The status returned from the GetDatabases call was NOT OK");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }

        }

        /*
         * Provision a VDB
         * https://{{yourDelphixEnvironment}}/api/#database
         * Input Type: ProvisionParameters
         * Response Example:
         * {
    "type": "OKResult",
    "status": "OK",
    "result": "MSSQL_DB_CONTAINER-82",
    "job": "JOB-600",
    "action": "ACTION-1620"
}
        */
        public DelphixResponse ProvisionVdb(DelphixEnvironment sourceEnv,     //Go through these and see what I can simplify.. ie: Shouldn't be passing sourceDbRef as a string and also the DelphixDb of the sourceDb
        DelphixSourceConfig sourceSourceConfig,
        DelphixRepository destinationRepo,
        DelphixTimeFlow snapshot,
        string sourceDbRef,
        DelphixDatabase sourceDb,
        string groupRefString,
        string dbName,
        bool wait = false
        )
        {

                //dbName = sourceDb.name.Substring(sourceDb.name.LastIndexOf('-') + 1).Trim();
          

            dynamic container = new JObject();
            container.name = dbName; //Fix this
            container.type = "MSSqlDatabaseContainer";
            container.group = groupRefString.ToString();

            dynamic source = new JObject();
            source.type = "MSSqlVirtualSource";
            source.config = sourceSourceConfig.reference.ToString();
            source.allowAutoVDBRestartOnHostReboot = true;

            dynamic sourceConfig = new JObject();
            sourceConfig.type = "MSSqlSIConfig";
            sourceConfig.databaseName = dbName;  //Fix this
            sourceConfig.repository = destinationRepo.reference.ToString();

            dynamic timeflowPointParameters = new JObject();
            timeflowPointParameters.type = "TimeflowPointSemantic";
            timeflowPointParameters.location = "LATEST_SNAPSHOT";
            timeflowPointParameters.container = sourceDbRef;
            //timeflowPointParameters.timeflow = snapshot.reference;

            dynamic ProvisionParameters = new JObject();
            ProvisionParameters.container = container;
            ProvisionParameters.source = source;
            ProvisionParameters.sourceConfig = sourceConfig;
            ProvisionParameters.timeflowPointParameters = timeflowPointParameters;
            ProvisionParameters.type = "MSSqlProvisionParameters";


            ProvisionParameters = JsonConvert.SerializeObject(ProvisionParameters);
            var request = new RestRequest("resources/json/delphix/database/provision", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(ProvisionParameters);

            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            logger.Info(request.ToString());
            try
            {
                var result = Session.delphixClient.Post(request);
                string dbs = result.Content;
                Console.WriteLine(ProvisionParameters);
                var response = JsonConvert.DeserializeObject<ProvisionVdbResponse>(dbs);
                if (response.status.Equals("OK"))
                {
                    var deserializedDbs = response.job;
                    logger.Info(deserializedDbs.ToString());

                    //If we want to wait until the Job is completed...
                    if (wait)
                    {
                        JobService jobHelper = new JobService();
                        DelphixJob completedJob = jobHelper.GetJobByRefAndWait(deserializedDbs);
                        jobHelper.Dispose();
                        //return completedJob.parentActionState;
                        return response;
                    }


                    return response;
                }
                else
                {
                    //This means there was an error actually creating a job to provision a Vdb.  Check Request Body + if Delphix was reachable. 
                    Console.WriteLine("The status returned from the ProvisionVDBs call was NOT OK");
                    logger.Error("There was an error creating a Job for the ProvisionVDB call.  The response status was: " + response.status + "Request Body:");
                    logger.Info(ProvisionParameters.ToString());
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

        public DeleteVdbResponse DeleteVdb(DelphixDatabase dbToDelete)
        {

            var request = new RestRequest("resources/json/delphix/database/" + dbToDelete.reference.ToString(), Method.DELETE);
            request.RequestFormat = DataFormat.Json;


            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);

            var result = Session.delphixClient.Delete(request);
            string dbs = result.Content;

            var response = JsonConvert.DeserializeObject<DeleteVdbResponse>(dbs);
            if (response.status.Equals("OK"))
            {
                var deserializedDbs = response.job;
                //return deserializedDbs;
                return response;
            }
            else
            {
                Console.WriteLine("The status returned from the GetDatabases call was NOT OK");
                return response;
            }

        }

        #endregion

        #region Filters
        public List<DelphixDatabase> GetDatabasesByGroup(string groupReference, List<DelphixDatabase> dbs)
        {
            List<DelphixDatabase> groupDatabases = new List<DelphixDatabase>();
            foreach (DelphixDatabase delphixDb in dbs)
            {
                if (delphixDb.group.ToString().Equals(groupReference))
                {
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }

        public List<DelphixDatabase> GetDatabasesByName(string name, List<DelphixDatabase> dbs)
        {
            List<DelphixDatabase> groupDatabases = new List<DelphixDatabase>();
            foreach (DelphixDatabase delphixDb in dbs)
            {
                if (delphixDb.name.ToString().Equals(name))
                {
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }
        #endregion

    }
}
