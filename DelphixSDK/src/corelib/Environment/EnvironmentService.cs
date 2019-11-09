﻿using System;
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
using DelphixLibrary.Job;

namespace DelphixLibrary.Environment
{
    public class EnvironmentService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #region API Calls
        public List<DelphixEnvironment> GetEnvironments()
        {

            //List<DelphixDatabase> deserializedDbList = new List<DelphixDatabase>();
            //string groups = "";
            var request = new RestRequest("resources/json/delphix/environment", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(body);
            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            logger.Info(request.ToString());
            try
            {
                var result = Session.delphixClient.Get(request);
                string dbs = result.Content;
                var response = JsonConvert.DeserializeObject<GetEnvironmentsResponse>(dbs);
                if (response.status.Equals("OK"))
                {
                    var deserializedDbs = response.result;
                    logger.Info(deserializedDbs.ToString());
                    return deserializedDbs;
                }
                else
                {
                    logger.Warn("The response was NOT OK GetEnvironments: " + response.result);
                    throw new Exception("The status returned from the GetEnvironments call was NOT OK");
                }
            }
            catch (Exception ex)
            {
                logger.Error("There was an error running GetEnvironments:" + ex);
                throw ex;
            }

        }

        public string CreateTargetEnv(string sqlServerName, string sqlServerIp, string serverPassword, string serverUser, string buildNumber, bool wait = false)
        {

            dynamic hostEnvironment = new JObject();
            hostEnvironment.description = "Generated by MultiRestore: " + buildNumber; //Fix this
            hostEnvironment.logCollectionEnabled = false ;
            hostEnvironment.type = "WindowsHostEnvironment";
            hostEnvironment.name = "TARGET - " + sqlServerName;

            dynamic host = new JObject();
            host.address = sqlServerIp;
            host.connectorPort = 9100;
            host.javaHome = null;
            host.sshPort = 22;
            host.type = "WindowsHost";

            dynamic hostParameters = new JObject();
            hostParameters.type = "WindowsHostCreateParameters";
            hostParameters.host = host;

            dynamic credential = new JObject();
            credential.password = serverPassword;
            credential.type = "PasswordCredential";  //Fix this

            dynamic primaryUser = new JObject();
            primaryUser.type = "EnvironmentUser";
            primaryUser.name = serverUser;
            primaryUser.credential = credential;

            dynamic ProvisionParameters = new JObject();
            ProvisionParameters.hostEnvironment = hostEnvironment;
            ProvisionParameters.hostParameters = hostParameters;
            ProvisionParameters.primaryUser = primaryUser;
            ProvisionParameters.logCollectionEnabled = false;
            ProvisionParameters.type = "HostEnvironmentCreateParameters";




            ProvisionParameters = JsonConvert.SerializeObject(ProvisionParameters);
            var request = new RestRequest("resources/json/delphix/environment", Method.POST);
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
                var response = JsonConvert.DeserializeObject<CreateEnvironmentResponse>(dbs);
                if (response.status.Equals("OK"))
                {
                    var deserializedDbs = response.job.ToString();
                    logger.Info(deserializedDbs);

                    //If we want to wait until the Job is completed...
                    if (wait)
                    {
                        JobService jobHelper = new JobService();
                        DelphixJob completedJob = jobHelper.GetJobByRefAndWait(deserializedDbs);
                        jobHelper.Dispose();
                        return completedJob.parentActionState;
                    }
                    return deserializedDbs;
                }
                else
                {
                    //This means there was an error actually creating a job to create a target.  Check Request Body + if Delphix was reachable. 
                    Console.WriteLine("The status returned from the CreateTargetEnv call was NOT OK");
                    logger.Error("There was an error creating a Job for the CreateTargetEnv call.  The response status was: " + response.status + "Request Body:");
                    logger.Info(ProvisionParameters.ToString());

                    return response.status;
                }
            }
            catch (Exception ex)
            {
                logger.Error("There was an error running CreateTargetEnvironments:" + ex);
                logger.Warn("JSON Body: " + ProvisionParameters);
                
                throw ex;
            }
        }
        public async Task<string> CreateTargetEnvAsync(string sqlServerName, string sqlServerIp, string serverPassword, string serverUser, string buildNumber)
        {
            logger.Info("ASYNC Target Creation Requested for " + sqlServerName );
            return await Task.Run(() => CreateTargetEnv(sqlServerName,sqlServerIp,serverPassword,serverUser,buildNumber));
        }
        #endregion
        #region Filters 
        public List<DelphixEnvironment> GetEnvironmentsByHostName(string hostName, List<DelphixEnvironment> sourceConfigs)
        {
            List<DelphixEnvironment> groupDatabases = new List<DelphixEnvironment>();
            foreach (DelphixEnvironment delphixDb in sourceConfigs)
            {
                if (delphixDb.host.ToString().Equals(hostName))
                {
                    logger.Info("Found a match for " + hostName + "- RefId:" + delphixDb.reference.ToString() + " Name:" + delphixDb.name.ToString());
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }

        public List<DelphixEnvironment> GetEnvironmentsByPartialName(string Name, List<DelphixEnvironment> sourceConfigs)
        {
            List<DelphixEnvironment> groupDatabases = new List<DelphixEnvironment>();
            foreach (DelphixEnvironment delphixDb in sourceConfigs)
            {
                if (delphixDb.name.ToString().Contains(Name))
                {
                    logger.Info("Found a match for " + Name + "- RefId:" + delphixDb.reference.ToString() + delphixDb.name.ToString());
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }

        public List<DelphixEnvironment> GetEnvironmentsByName(string Name, List<DelphixEnvironment> sourceConfigs)
        {
            List<DelphixEnvironment> groupDatabases = new List<DelphixEnvironment>();
            foreach (DelphixEnvironment delphixDb in sourceConfigs)
            {
                Name = Name.ToUpper();
                if (delphixDb.name.ToString().ToUpper().Contains(Name))
                {
                    logger.Info("Found a match for " + Name + "- RefId:" + delphixDb.reference.ToString() + " Name:" + delphixDb.name.ToString());
                    groupDatabases.Add(delphixDb);
                }
            }

            return groupDatabases;

        }

        public string ParseServerName(string databaseName) {
            string parsedServerName = "";
            try {
                parsedServerName = databaseName.Substring(databaseName.LastIndexOf('-') - 1);
            }
            catch (Exception ex){
                Console.WriteLine(databaseName + ": Did not match a server name ");
                Console.WriteLine(ex);
            }
            return parsedServerName;
        }
        public void GetTargetEnvironments(List<DelphixEnvironment> environments)
        {
                
        }
        #endregion
    }
}
