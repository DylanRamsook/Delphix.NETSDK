using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary.Authentication;
using DelphixLibrary.Database;
using DelphixLibrary.Environment;
using DelphixLibrary.Group;
using DelphixLibrary.Repository;
using DelphixLibrary.Source;
using DelphixLibrary.SourceConfig;
using DelphixLibrary.Template;
using DelphixLibrary.Timeflow;
using DelphixLibrary.Job;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace DelphixUnitTests
{
    [TestClass]
    public class DatabaseServiceTests
    {

        [TestMethod]
        //Check that create a VDB call will return a response.
        public void ProvisionVdbTest()
        {
            // Create and valiate a session
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            // Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);

            // Get a cookie + Authenticate
            Session.CreateSession(System.Environment.GetEnvironmentVariable("DELPHIX_USER"), System.Environment.GetEnvironmentVariable("DELPHIX_PASSWORD"), System.Environment.GetEnvironmentVariable("DELPHIX_URL"));
            string cookie = Session.jSessionId.Value;


            //Create helpers
            GroupService groupHelper = new GroupService();
            EnvironmentService delphixEnvironmentHelper = new EnvironmentService();
            SourceConfigService sourceConfigHelper = new SourceConfigService();
            RepositoryService repositoryHelper = new RepositoryService();
            TimeflowService timeflowHelper = new TimeflowService();
            SourceService sourceHelper = new SourceService();
            DatabaseService dbHelp = new DatabaseService();

            //Create a test group
            string newGroupRef = groupHelper.CreateGroups("intTestGroup", true).result;
            if (newGroupRef.Equals("Group already exists"))
            {
                Console.WriteLine("A group named intTestGroup already exists.  An attempt to delete the existing one was made, but there are active databases within the group.");
            }

            //Create a target server on Delphix






        }



    }
}

