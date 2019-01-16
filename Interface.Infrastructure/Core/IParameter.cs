
using System.Collections.Generic;
using Interface.Infrastructure.Entities;

namespace Interface.Infrastructure.Core
{
    public interface IParameter : IHandler
    {
        Dictionary<string, object> GlobalParameters();
    }

    public interface IDownParameter : IParameter
    {
        Dictionary<string, object> DownParameters(int sequence);
        HttpParameters DownHttpRequest(int sequence, string[] parameter);
    }
    public interface IUpParameter : IParameter
    {
        Dictionary<string, object> UpParameters(int sequence);
        HttpParameters UpHttpRequest(int sequence, string[] parameter);
    }

    public interface ISapParameter : IParameter
    {
        SapParameters SapParameters();
    }
}
