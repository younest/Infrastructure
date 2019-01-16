using Common.Logging;

using Interface.Infrastructure.Core;
using Interface.Infrastructure.Entities;

namespace Sample
{
    public class InterfaceHandler : IDownHandler
    {
        private static InterfaceHandler _instance = null;

        private string dbConnecString = string.Empty;

        private static ILog logeer = LogManager.GetLogger("InterfaceHandler");

        private InterfaceHandler()
        {
            dbConnecString = (string)InterfaceParameter.Instance.GetGlobalParameters()["DB"];
        }

        public static InterfaceHandler Instance
        {
            get => _instance ?? (new InterfaceHandler());
        }

        public InterfaceResult<TEntity> DownEntities<TEntity>(InterfaceRequest request) where TEntity : class, new()
        {
            throw new System.NotImplementedException();
        }

        public void Throw()
        {
            throw new System.NotImplementedException();
        }
    }
}
