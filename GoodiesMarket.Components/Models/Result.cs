using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace GoodiesMarket.Components.Models
{
    [DataContract]
    public class Result
    {
        public Result()
        {
            Errors = new HashSet<string>();
        }

        [DataMember]
        public JToken Response { get; set; }

        [DataMember]
        public bool Succeeded { get; set; }

        [IgnoreDataMember]
        public HttpStatusCode Status { get; set; }

        [IgnoreDataMember]
        public ICollection<string> Errors { get; set; }
    }
}
