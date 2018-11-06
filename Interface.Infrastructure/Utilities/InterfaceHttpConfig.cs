using System.Text;
using System.Collections.Generic;
using Interface.Infrastructure.Entities;
using SAP.Middleware.Connector;


namespace Interface.Infrastructure.Utilities
{
    public class InterfaceHttpConfig
    {
        private static string FromSoapToString(SoapParameter soap)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<{0}:Envelope ", soap.RootNode);

            sb.Append("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
            sb.Append("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" ");
            sb.AppendFormat("xmlns:{0}=\"http://schemas.xmlsoap.org/soap/envelope/\" ", soap.RootNode);

            if (soap.RootDefaultNameSpace != string.Empty)
                sb.Append(soap.RootDefaultNameSpace).Append(">");

            if (soap.RootDefaultNameSpace == string.Empty)
                sb.Append(">").AppendFormat("<{0}:Header/>", soap.RootNode);

            sb.AppendFormat("<{0}:Body>", soap.RootNode);

            if (soap.MethodDefaultNameSpace != string.Empty)
                sb.AppendFormat("<{0} {1}>", soap.MethodNode, soap.MethodDefaultNameSpace);

            if (soap.MethodDefaultNameSpace == string.Empty)
                sb.AppendFormat("<{0}>", soap.MethodNode);

            sb.AppendFormat("{0}", soap.MethodParameterValue);
            sb.AppendFormat("</{0}>", soap.MethodNode);
            sb.AppendFormat("</{0}:Body>", soap.RootNode);

            sb.AppendFormat("</{0}:Envelope>", soap.RootNode);

            return sb.ToString();
        }

        public static HttpParameters Setting(string url, string request, DataFormatter formatter)
        {
            HttpParameters parameter = new HttpParameters();
            parameter.Url = url;
            switch (formatter)
            {
                case DataFormatter.SOAP:
                    parameter.ContentType = ContentType.Soap;
                    //parameter.soapAction = input.SoapAction;
                    parameter.ParameterValue = request;
                    break;
                case DataFormatter.OData:
                    parameter.ContentType = ContentType.Soap;
                    parameter.soapAction = "";
                    parameter.ParameterValue = request;
                    break;
                case DataFormatter.XML:
                    parameter.ContentType = ContentType.Xml;
                    parameter.soapAction = "";
                    parameter.ParameterValue = request;
                    break;
                case DataFormatter.JSON:
                    parameter.ContentType = ContentType.Json;
                    parameter.soapAction = "";
                    parameter.ParameterValue = request;
                    break;
                default:
                    break;
            }
            return parameter;
        }

        public static SapParameters Setting(string destinationsName, string functionName, string tableName, Dictionary<string, string> parameter)
        {
            SapParameters sap = new SapParameters();

            sap.Parameter = parameter;
            sap.TableName = tableName;
            sap.FunctionName = functionName;
            sap.ServerHost = RfcDestinationManager.GetDestination(destinationsName);

            return sap;
        }
    }
}
