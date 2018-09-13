using Common.Logging;

using System.Configuration;
using System.Collections.Generic;

using Interface.Infrastructure.Core;
using Interface.Infrastructure.Entities;
using Interface.Infrastructure.Utilities;

namespace Sample
{
    public class InterfaceConfig : IConfig
    {
        private static InterfaceConfig _instance = null;

        private static ILog logeer = LogManager.GetLogger("InterfaceConfig");

        private InterfaceConfig() { }

        public static InterfaceConfig Instance
        {
            get => _instance ?? (new InterfaceConfig());
        }

        public Dictionary<string, object> GetParameter()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("mail", (InterfaceMailEntity)ConfigurationManager.GetSection("mailSettings"));
            dic.Add("DB", ConfigurationManager.ConnectionStrings["SFA_ID"].ConnectionString);
            dic.Add("POS", ConfigurationManager.AppSettings["POS"]);

            return dic;
        }

        public IParameter GetRequestParameters(int sequence, string[] parameter)
        {
            IParameter query = null;

            switch (sequence)
            {
                case (int)ServiceName.UserService:
                    query = InterfaceParameterSettings.Setting(parameter[0], parameter[1], DataFormatter.JSON);
                    break;
                case (int)ServiceName.CustomerService:
                    query = InterfaceParameterSettings.Setting(parameter[0], parameter[1], DataFormatter.XML);
                    break;
                default:
                    break;
            }

            return query;
        }
    }
}
