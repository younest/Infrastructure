
using Interface.Infrastructure.Entities;

namespace Interface.Infrastructure.Core
{
    public interface IHandler
    {
        void Throw();
    }

    public interface IGet : IHandler
    {
        InterfaceResult<TEntity> GetEntities<TEntity>(InterfaceRequest request) where TEntity : class, new();
    }

    public interface IPost : IHandler
    {
        InterfaceResult<TEntity> AddEntities<TEntity>(InterfaceRequest request) where TEntity : class, new();
    }

    public interface IPut : IHandler
    {
        InterfaceResult<TEntity> UpdateEntities<TEntity>(InterfaceRequest request) where TEntity : class, new();
    }
}
