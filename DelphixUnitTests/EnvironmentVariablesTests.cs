using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DelphixLibrary.Authentication;
using System.Net;

using DelphixLibrary;
namespace DelphixUnitTests
{
    [TestClass]
    public class EnvironmentVariablesTests
    {

        // If these fail, the rest of the tests will fail.  
        [TestMethod]
        public void DelphixUserEnvironmentVariableIsSet()
        {
            EnvironmentVariables.SetDelphixEnvironmentVariables();
            Assert.IsNotNull(EnvironmentVariables.DelphixUser);
        }

        public void DelphixPasswordEnvironmentVariableIsSet()
        {
            EnvironmentVariables.SetDelphixEnvironmentVariables();
            Assert.IsNotNull(EnvironmentVariables.DelphixPassword);
        }

        public void DelphixUrlEnvironmentVariableIsSet()
        {
            EnvironmentVariables.SetDelphixEnvironmentVariables();
            Assert.IsNotNull(EnvironmentVariables.DelphixUrl);
        }
    }
}
