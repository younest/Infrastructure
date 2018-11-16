﻿using Interface.Infrastructure.Entities;
using System.Collections.Generic;

namespace Interface.Infrastructure.Core
{
    public interface IAccess
    {
        string ToSerialization<TEntity>(TEntity obj);
        List<TEntity> ToDeserialization<TEntity>(int sequence, string obj);
    }

    public interface IDownload
    {
        InterfaceResult<TEntity> DownRequestHandler<TEntity>(int sequence, HttpParameters http) where TEntity : class, new();
        InterfaceResult<TEntity> DownResponseHandler<TEntity>(InterfaceRequest request) where TEntity : class, new();
    }
    public interface IUpload
    {
        InterfaceResult<TEntity> UpRequestHandler<TEntity>(int sequence, HttpParameters http) where TEntity : class, new();
        InterfaceResult<TEntity> UpResponseHandler<TEntity>(InterfaceRequest request) where TEntity : class, new();
    }
}
