
using Interface.Infrastructure.Entities;

namespace Interface.Infrastructure.Core
{
    public interface IHandler
    {
        void Throw();
    }

    public interface IGet : IHandler
    {
        InterfaceResult<T> GetEntities<T>(InterfaceRequest request) where T : class, new();
    }

    public interface IPost : IHandler
    {
        InterfaceResult<T> AddEntities<T>(InterfaceRequest request) where T : class, new();
    }

    public interface IPut : IHandler
    {
        InterfaceResult<T> UpdateEntities<T>(InterfaceRequest request) where T : class, new();
    }
}
