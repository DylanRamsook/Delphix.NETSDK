using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DelphixLibrary.Authentication;
using System.Net;

namespace DelphixUnitTests
{
    [TestClass, TestCategory("Functional")]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateSessionTest()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
            Session.CreateSession(System.Environment.GetEnvironmentVariable("DELPHIX_USER"), System.Environment.GetEnvironmentVariable("DELPHIX_PASSWORD"), System.Environment.GetEnvironmentVariable("DELPHIX_URL"));
            string cookie = Session.jSessionId.Value;

            Assert.IsNotNull(cookie);
        }
    }
}
