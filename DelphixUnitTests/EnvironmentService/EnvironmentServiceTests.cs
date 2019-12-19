using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DelphixLibrary;
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
    public class EnvironmentServiceTests
    {

        [TestMethod, TestCategory("Functional")]
        //Check that create a VDB call will return a response.
        public void FilterEnvironmentsByNameTest()
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
            EnvironmentService envService = new EnvironmentService();
            DelphixDatabase vdb = new DelphixDatabase();
            List<DelphixEnvironment> allDbs = new List<DelphixEnvironment>();
            allDbs = envService.GetEnvironments();
            DelphixEnvironment aVdb = envService.GetEnvironmentsByName("ctt24co1",allDbs).First();
            allDbs.Clear();
            allDbs.Add(aVdb);








        }



    }
}

