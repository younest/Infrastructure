
using System.Xml;
using System.Text;

using Interface.Infrastructure.Entities;
using Interface.Infrastructure.Utilities;


namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //json http
            HttpRequestSample();

            //soap http
            //HttpSoapSample();

            //InterfaceRequest
            //InterfaceRequestSample();

            //SendMailSample
            //InterfaceSendMailSample();
        }

        static void HttpSoapSample()
        {
            //获取配置参数
            string[] p = new string[] { (string)InterfaceParameter.Instance.GetConfigParameters()["POS"],string.Empty,
                                        System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")};

            //创建Soap实体对象并添加参数
            SoapParameter soap = new SoapParameter();

            StringBuilder sb = new StringBuilder();
            //添加根节点
            soap.RootNode = "soapenv";
            soap.RootDefaultNameSpace = "xmlns:ws=\"http://ws.nip.com\"";
            soap.SoapAction = "";
            //添加函数API
            soap.MethodNode = "ws:shopSystemInspectData";
            soap.MethodDefaultNameSpace = "soapenv:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\"";
            //添加参数
            sb.AppendFormat("<shopcode xsi:type=\"xsd:string\">{0}</shopcode>", p[1]);
            sb.AppendFormat("<idate xsi:type=\"xsd:string\">{0}</idate>", p[2]);
            //返回Soap信息
            soap.MethodParameterValue = sb.ToString();
     
            //创建请求对象
            HttpParameters query = InterfaceHttpConfig.Setting(p[0], soap.ToString(), DataFormatter.SOAP);

            //调用接口获取返回信息；
            HttpParameters http = (HttpParameters)query;
            XmlDocument doc = InterfaceHttpRequest.QueryXml(http);
        }

        static void HttpRequestSample()
        {
            HttpParameters p = new HttpParameters();

            p.Url = "http://dmscn-m-dev.carlsberg.asia/Carlsberg/Service.svc/Customer/GetCustomerChain";
            p.Method = RequestMethod.Post;
            p.ContentType = ContentType.Json;
            p.ParameterValue = "";

            string result = InterfaceHttpRequest.Query(p);
        }

        static void InterfaceRequestSample()
        {
            int sequence = (int)ServiceName.UserService;
            string url = (string)InterfaceParameter.Instance.GetConfigParameters()["POS"];

            string[] p = new string[] { url, string.Empty };
            HttpParameters query = InterfaceParameter.Instance.GetHttpParameters(sequence, p);

            InterfaceRequest request = new InterfaceRequest();
            request.Sequence = sequence;
            request.Parameter.Add(query);

            InterfaceResult<UserEntity> result = InterfaceHandler.Instance.GetEntities<UserEntity>(request);
            if (result.Status == Status.Failed) InterfaceHandler.Instance.Throw();

        }
    }
}
