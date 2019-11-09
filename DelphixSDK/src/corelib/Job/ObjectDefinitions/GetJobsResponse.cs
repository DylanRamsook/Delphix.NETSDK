using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using DelphixLibrary.Job;
using Newtonsoft.Json.Linq;

namespace DelphixLibrary
{
    class GetJobsResponse : DelphixResponse
    {
        //public DelphixJob result { get; set; }
        [JsonProperty("result")]
        [JsonConverter(typeof(SingleOrArrayConverter<DelphixJob>))]
        public List<DelphixJob> result { get; set; }
        public string job { get; set; }
        public string action { get; set; }
    }

    internal class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
