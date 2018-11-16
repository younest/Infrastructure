
using System;
using System.Text;
using System.Xml.Serialization;

using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using System.Xml;

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

        public static string JsonSerialize(object value, Newtonsoft.Json.Formatting formatting)
        {
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            jsetting.NullValueHandling = NullValueHandling.Ignore;

            return JsonConvert.SerializeObject(value, formatting, jsetting);
        }

        public static TEntity JsonDeserialize<TEntity>(string json)
        {
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            jsetting.NullValueHandling = NullValueHandling.Ignore;

            return (TEntity)JsonConvert.DeserializeObject<TEntity>(json, jsetting);
        }

        public static string XmlSerializer<TEntity>(TEntity obj)
        {
            string xmlString = string.Empty;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                XmlWriterSettings setting = new XmlWriterSettings();
                setting.Encoding = new UTF8Encoding(false);
                setting.Indent = true;
                setting.OmitXmlDeclaration = true;

                using (XmlWriter writer = XmlWriter.Create(memoryStream, setting))
                {
                    xmlSerializer.Serialize(writer, obj, namespaces);
                }
                xmlString = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return xmlString;
        }

        public static string XmlSerializer<TEntity>(TEntity obj, bool isNameSpaces)
        {
            string xmlString = string.Empty;

            if (isNameSpaces) { return WcfXmlSerializer<TEntity>(obj); }

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (StringWriter writer = new StringWriter())
            {
                new XmlSerializer(obj.GetType()).Serialize((TextWriter)writer, obj, ns);
                xmlString = writer.ToString();
            }
            return xmlString;
        }

        public static string XmlSerializer<TEntity>(TEntity obj, Encoding encoding)
        {
            string xmlString = string.Empty;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, encoding);
                xmlTextWriter.Formatting = System.Xml.Formatting.None;

                xmlSerializer.Serialize(xmlTextWriter, obj, namespaces);

                xmlTextWriter.Flush();
                xmlTextWriter.Close();

                xmlString = encoding.GetString(memoryStream.ToArray());
            }
            return xmlString;
        }

        public static TEntity XmlDeserialize<TEntity>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TEntity));
            using (StringReader reader = new StringReader(xml))
            {
                return (TEntity)serializer.Deserialize(reader);
            }
        }

        private static string WcfXmlSerializer<TEntity>(TEntity obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer data = new DataContractSerializer(typeof(TEntity));
                data.WriteObject(ms, obj);
                ms.Position = 0;
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }

}
