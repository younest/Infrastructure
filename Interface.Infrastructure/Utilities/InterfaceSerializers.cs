
using System;
using System.Text;
using System.Xml.Serialization;

using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Interface.Infrastructure.Utilities
{
    public class InterfaceSerializers
    {
        public static string FromBase64ToString(string str)
        {
            string compressString = string.Empty;
            byte[] compressBeforeByte = Convert.FromBase64String(str);
            byte[] compressAfterByte = Decompress(compressBeforeByte);
            compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);
            return compressString;
        }

        private static byte[] Decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);
                MemoryStream msreader = new MemoryStream();
                byte[] buffer = new byte[0x1000];
                while (true)
                {
                    int reader = zip.Read(buffer, 0, buffer.Length);
                    if (reader <= 0)
                    {
                        break;
                    }
                    msreader.Write(buffer, 0, reader);
                }
                zip.Close();
                ms.Close();
                msreader.Position = 0;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string JsonSerialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static T JsonDeserialize<T>(string json)
        {
            return (T)JsonConvert.DeserializeObject<T>(json);
        }

        public static string XmlSerializer<T>(T obj, bool isNameSpaces)
        {
            string xmlString = string.Empty;

            if (isNameSpaces) { return WcfXmlSerializer<T>(obj); }

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (StringWriter writer = new StringWriter())
            {

                new XmlSerializer(obj.GetType()).Serialize((TextWriter)writer, obj, ns);
                xmlString = writer.ToString();
            }
            return xmlString;
        }

        public static T XmlDeserialize<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        private static string WcfXmlSerializer<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer data = new DataContractSerializer(typeof(T));
                data.WriteObject(ms, obj);
                ms.Position = 0;
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
