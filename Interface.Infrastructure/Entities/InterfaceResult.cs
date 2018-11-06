
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Interface.Infrastructure.Entities
{
    public class InterfaceResult<T>
    {
        [DataMember(Order = 0)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [DataMember(Order = 1)]
        public string Message { get; set; }

        [DataMember(Order = 2)]
        public List<T> Result { get; set; }

        public InterfaceResult()
        {
            Result = new List<T>();
        }
    }
}
