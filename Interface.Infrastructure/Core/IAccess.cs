using Interface.Infrastructure.Entities;
using System.Collections.Generic;

namespace Interface.Infrastructure.Core
{
    public interface IAccess
    {
        string ToSerialization<T>(T obj);
        List<T> ToDeserialization<T>(int sequence, string obj);
    }

    public interface IDownload
    {
        InterfaceResult<T> DownRequestHandler<T>(int sequence, HttpParameters http) where T : class, new();
        InterfaceResult<T> DownResponseHandler<T>(InterfaceRequest request) where T : class, new();
    }
    public interface IUpload
    {
        InterfaceResult<T> UpRequestHandler<T>(int sequence, HttpParameters http) where T : class, new();
        InterfaceResult<T> UpResponseHandler<T>(InterfaceRequest request) where T : class, new();
    }
}
