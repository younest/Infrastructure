using Common.Logging;

using System.Configuration;
using System.Collections.Generic;

using Interface.Infrastructure.Core;
using Interface.Infrastructure.Entities;
using Interface.Infrastructure.Utilities;

namespace Sample
{
    public class InterfaceParameter : IParameter
    {
        private static InterfaceParameter _instance = null;

        private static ILog logeer = LogManager.GetLogger("InterfaceConfig");

        private InterfaceParameter() { }

        public static InterfaceParameter Instance
        {
            get => _instance ?? (new InterfaceParameter());
        }

        public Dictionary<string, object> GetConfigParameters()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("mail", (InterfaceMailEntity)ConfigurationManager.GetSection("mailSettings"));
            dic.Add("DB", ConfigurationManager.ConnectionStrings["SFA_ID"].ConnectionString);

            return dic;
        }

        public HttpParameters GetHttpParameters(int sequence, string[] parameter)
        {
            HttpParameters http = new HttpParameters();

            switch (sequence)
            {
                case (int)ServiceName.UserService:
                    http = InterfaceHttpConfig.Setting(parameter[0], parameter[1], DataFormatter.JSON);
                    break;
                case (int)ServiceName.CustomerService:
                    http = InterfaceHttpConfig.Setting(parameter[0], parameter[1], DataFormatter.XML);
                    break;
                default:
                    break;
            }

            return http;
        }


        public Dictionary<string, object> GetParameters(int sequence)
        {
            throw new System.NotImplementedException();
        }


        public SapParameters GetSapParameters()
        {
            throw new System.NotImplementedException();
        }
    }
}
