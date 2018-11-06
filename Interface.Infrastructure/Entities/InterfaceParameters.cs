using SAP.Middleware.Connector;
using System.Collections.Generic;
using System.Net;

namespace Interface.Infrastructure.Entities
{

    public class HttpParameters
    {
        public string Url { get; set; }

        public string ContentType { get; set; }

        public string soapAction { get; set; }

        public string ParameterValue { get; set; }

        public ICredentials Authorization { get; set; }

        public RequestMethod Method { get; set; }
    }

    public class SapParameters
    {
        public string FunctionName { get; set; }

        public string TableName { get; set; }

        public RfcDestination ServerHost { get; set; }

        public Dictionary<string, string> Parameter { get; set; }
    }

    public class SoapParameter
    {
        public string RootNode { get; set; }

        public string RootDefaultNameSpace { get; set; }

        public string SoapAction { get; set; }

        public string MethodNode { get; set; }

        public string MethodDefaultNameSpace { get; set; }

        public string MethodParameterValue { get; set; }
    }
}
