using Interface.Infrastructure.Entities;
using Interface.Infrastructure.Utilities;

namespace Sample
{
    class Program
    {

        static void Main(string[] args)
        {
            //Dictionary<string, object> dic=InterfaceConfig.Instance.GetParameter();

            //string[] p = new string[] { (string)InterfaceConfig.Instance.GetParameter()["POS"],string.Empty,
            //                            System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")};

            //IParameter query = null;
            //SoapEntity soap = new SoapEntity();
            //StringBuilder sb = new StringBuilder();

            //soap.RootNode = "soapenv";
            //soap.RootDefaultNameSpace = "xmlns:ws=\"http://ws.nip.com\"";
            //soap.SoapAction = "";
            //soap.MethodNode = "ws:shopSystemInspectData";
            //soap.MethodDefaultNameSpace = "soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\"";
            //sb.AppendFormat("<shopcode xsi:type=\"xsd:string\">{0}</shopcode>", p[1]);
            //sb.AppendFormat("<idate xsi:type=\"xsd:string\">{0}</idate>", p[2]);
            //soap.MethodParameterValue = sb.ToString();

            //query = InterfaceParameterSettings.Setting(p[0], soap, DataFormatter.SOAP);

            //HttpParameters http = (HttpParameters)query;
            //XmlDocument doc = InterfaceHttpRequest.QueryXml(http);

            HttpParameters p = new HttpParameters();

            p.Url = "http://dmscn-m-dev.carlsberg.asia/Carlsberg/Service.svc/Customer/GetCustomerChain";
            p.Method = RequestMethod.Post;
            p.ContentType = ContentType.Json;
            p.ParameterValue = "{\"Query_Start\":\"2018 - 07 - 05\",\"Query_End\":\"\"}";

            string result= InterfaceHttpRequest.Query(p);
        }
    }
}
