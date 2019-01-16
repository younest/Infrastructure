
using Interface.Infrastructure.Entities;

namespace Interface.Infrastructure.Core
{
    public interface IHandler
    {
        void Throw();
    }

    public interface IDownHandler:IHandler
    {
        InterfaceResult<TEntity> DownEntities<TEntity>(InterfaceRequest request) where TEntity : class, new();
    }

    public interface IUpHandler:IHandler
    {
        InterfaceResult<TEntity> UpEntities<TEntity>(InterfaceRequest request) where TEntity : class, new();
    }
}
