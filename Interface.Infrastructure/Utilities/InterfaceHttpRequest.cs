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

        public static string Query(HttpParameters http)
        {
            string result = string.Empty;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                switch (http.Method)
                {
                    case RequestMethod.Get:
                        result = ToGet(http);
                        break;
                    case RequestMethod.Post:
                        result = ToPost(http);
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
            }
            return result;
        }

        public static XmlDocument QueryXml(HttpParameters http)
        {
            string result = Query(http);

            XmlDocument doc = new XmlDocument();

            if (IsConnec && !string.IsNullOrEmpty(result))
                doc.LoadXml(result);

            return doc;
        }

        private static string ToGet(HttpParameters http)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(http.Url + "/?" + http.ParameterValue);

            request.Timeout = 10 * 60 * 1000;
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = http.ContentType;
            request.Headers.Add("soapAction", http.soapAction);
            request.Credentials = http.Authorization;
            request.Accept = http.ContentType;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                _isConnec = true;
                return reader.ReadToEnd();
            }
        }

        private static string ToPost(HttpParameters http)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(http.Url);

            request.Timeout = 10 * 60 * 1000;
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = http.ContentType;
            request.Headers.Add("soapAction", http.soapAction);
            request.Credentials = http.Authorization;
            request.Accept = http.ContentType;

            byte[] data = Encoding.UTF8.GetBytes(http.ParameterValue);
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
