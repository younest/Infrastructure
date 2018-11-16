using Common.Logging;

using System.Data;
using System.Collections.Generic;

using Interface.Infrastructure.Core;
using Interface.Infrastructure.Entities;
using Interface.Infrastructure.Persistence;

namespace Sample
{
    public class InterfaceHandler : IGet
    {
        private static InterfaceHandler _instance = null;

        private string dbConnecString = string.Empty;

        private static ILog logeer = LogManager.GetLogger("InterfaceHandler");

        private InterfaceHandler()
        {
            dbConnecString = (string)InterfaceParameter.Instance.GetConfigParameters()["DB"];
        }

        public static InterfaceHandler Instance
        {
            get => _instance ?? (new InterfaceHandler());
        }

        public InterfaceResult<TEntity> GetEntities<TEntity>(InterfaceRequest request) where TEntity : class, new()
        {
            InterfaceResult<TEntity> result = new InterfaceResult<TEntity>();

            try
            {
                DataTable entities = new DataTable();
                DataHandler dh = new DataHandler(dbConnecString);

                switch (request.Sequence)
                {
                    case (int)ServiceName.UserService:
                        entities = dh.GetInterfaceData("");//调用存储过程
                        break;
                    case (int)ServiceName.CustomerService:
                        entities = dh.GetInterfaceData("");
                        break;
                    default:
                        break;
                }
                List<TEntity> list = DataConvert.GetListFromTable<TEntity>(entities);

                result.Status = Status.Successfully;
                result.Result = list;
            }
            catch (System.Exception ex)
            {
                result.Status = Status.Failed;
                result.Message = ex.Message;
            }
            logeer.Info(result.Message);
            return result;
        }

        public void Throw()
        {
            throw new System.NotImplementedException();
        }
    }
}
