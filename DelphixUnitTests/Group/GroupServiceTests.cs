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
    public class GroupServiceTests
    {

        [TestMethod, TestCategory("Functional")]
        //Check that create a VDB call will return a response.
        public void CreateGroupTest()
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


            //Create a test group
            CreateGroupsResponse newGroupRef = groupHelper.CreateGroups("CreateUnit", true);
            if (newGroupRef.Equals("Group already exists"))
            {
                Console.WriteLine("A group named CreateUnit already exists.  An attempt to delete the existing one was made, but there are active databases within the group.");
            }







        }



    }
}

