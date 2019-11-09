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

using DelphixLibrary.Authentication;

namespace DelphixLibrary.Job
{
    public class JobService : IDisposable
    {
        public List<DelphixJob> GetJobs()
        {
            //List<DelphixDatabase> deserializedDbList = new List<DelphixDatabase>();
            //string groups = "";
            var request = new RestRequest("resources/json/delphix/job", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(body);
            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            try
            {
                var result = Session.delphixClient.Get(request);
                string dbs = result.Content;
                var response = JsonConvert.DeserializeObject<GetJobsResponse>(dbs);
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

        public DelphixJob GetJobByRef(string jobReference)
        {
            var request = new RestRequest("resources/json/delphix/job/"+ jobReference, Method.GET);

            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            try
            {
                var result = Session.delphixClient.Get(request);
                string dbs = result.Content;
                var response = JsonConvert.DeserializeObject<GetJobsResponse>(dbs);
                if (response.status.Equals("OK"))
                {
                    var deserializedDbs = response.result.First();
                    return deserializedDbs;
                }
                else
                {
                    throw new Exception("The status returned from the GetJobByRef call was NOT OK");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DelphixJob GetJobByRefAndWait(string jobReference)
        {
            var request = new RestRequest("resources/json/delphix/job/" + jobReference, Method.GET);

            request.AddHeader("content-header", "application/json");
            request.AddCookie(Session.jSessionId.Name, Session.jSessionId.Value);
            try
            {
                var result = Session.delphixClient.Get(request);
                string dbs = result.Content;
                var response = JsonConvert.DeserializeObject<GetJobsResponse>(dbs);
                if (response.status.Equals("OK"))
                {
                    var deserializedResponse = response.result.First();

                    while (deserializedResponse.jobState.ToString().Equals("RUNNING")) {
                        Console.WriteLine("Job in progress. Waiting 30s...");
                        Thread.Sleep(30000);
                        return GetJobByRefAndWait(jobReference); } // If the job is still running, wait 30s
                    return deserializedResponse;
                }
                else
                {
                    throw new Exception("The status returned from the GetJobByRef call was NOT OK");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<DelphixJob> GetJobByRefAndWaitAsync(string jobReference)
        {
            return await Task.Run(() => GetJobByRefAndWait(jobReference));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~JobService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}
