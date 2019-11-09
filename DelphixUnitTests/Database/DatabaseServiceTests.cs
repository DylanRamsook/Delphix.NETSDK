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
    public class ProvisionVdbSmokeTest
    {
        [TestMethod]
        //Create and validate a session
        public void CreateSessionTest()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
            Session.CreateSession("admin", "yourpassword", "yourinstancename");
            string cookie = Session.jSessionId.Value;

            Assert.IsNotNull(cookie);
        }


        [TestMethod]
        //Check that create a VDB call will return a response.
        public void ProvisionVdbTest()
        {
            //Create and valiate a session
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
            Session.CreateSession("admin", "yourpassword", "yourinstancename");
            string cookie = Session.jSessionId.Value;




        }



    }
}

