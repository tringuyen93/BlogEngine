using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BlogEngine.WebApi.ViewModel
{
    [DataContract]
    public class ResponseMetadata
    {
        [DataMember]
        public int StatusCode { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public ValidationResultModel ResponseException { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }
        [DataMember]
        public DateTime Timestamp { get; set; }

        public ResponseMetadata(int statusCode, string message = "", object result = null, ValidationResultModel error = null)
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
            ResponseException = error;
            Timestamp = DateTime.Now;
        }
    }
}
