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

using DelphixLibrary;
using DelphixLibrary.Authentication;
using DelphixLibrary.Generator;

namespace DelphixLibrary.Generator.templates
{
    class ServiceTemplate
    {
        public List<TemplateObject> GetTemplateObjects()
        {
            var request = new RestRequest("resources/json/delphix/environment", Method.GET);
            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            try
            {
                var result = Session.delphixClient.Get(request);
                string obj = result.Content;
                var response = JsonConvert.DeserializeObject<ListResultTemplateObject>(obj);
                if (response.status.Equals("OK"))
                {
                    var objs = response.result;
                    return objs;
                }
                else
                {
                    throw new Exception("The status returned from the api call was NOT OK");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
