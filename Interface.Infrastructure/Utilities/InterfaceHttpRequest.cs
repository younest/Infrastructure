using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using Common.Logging;
using Interface.Infrastructure.Entities;


namespace Interface.Infrastructure.Utilities
{
    public class InterfaceHttpRequest
    {
        private static bool _isConnec;

        public static bool IsConnec
        {
            get
            {
                return _isConnec;
            }
        }

        private static ILog logeer = LogManager.GetLogger("InterfaceHttpRequest");
 
        public static string Query(HttpParameters Parameters)
        {
            string result = string.Empty;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                switch (Parameters.Method)
                {
                    case RequestMethod.Get:
                        result = Get(Parameters);
                        break;
                    case RequestMethod.Post:
                        result = Post(Parameters);
                        break;
                    case RequestMethod.Put:
                        break;
                    case RequestMethod.Delete:
                        break;
                    default:
                        break;
                }
            }
            catch (WebException wex)
            {
                _isConnec = false;

                if (result.Length == 0)
                    result = wex.Message;

                logeer.InfoFormat("{0}", result);
            }
            return result;
        }

        public static XmlDocument QueryXml(HttpParameters Parameters)
        {
            string result = Query(Parameters);

            XmlDocument doc = new XmlDocument();

            if (IsConnec && !string.IsNullOrEmpty(result))
                doc.LoadXml(result);

            return doc;
        }

        private static string Get(HttpParameters Parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Parameters.Url + "/?" + Parameters.ParameterValue);

            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = Parameters.ContentType;
            request.Headers.Add("soapAction", Parameters.soapAction);
            request.Credentials = Parameters.Authorization;
            request.Accept = Parameters.ContentType;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                _isConnec = true;
                return reader.ReadToEnd();
            }
        }

        private static string Post(HttpParameters Parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Parameters.Url);

            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = Parameters.ContentType;
            request.Headers.Add("soapAction", Parameters.soapAction);
            request.Credentials = Parameters.Authorization;
            request.Accept = Parameters.ContentType;

            byte[] data = Encoding.UTF8.GetBytes(Parameters.ParameterValue);
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                    _isConnec = true;
                    return reader.ReadToEnd();
                }
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}
