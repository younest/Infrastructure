
using System.Collections.Generic;

using Interface.Infrastructure.Entities;

namespace Interface.Infrastructure.Core
{
    public interface IParameter
    {
        Dictionary<string, object> GetConfigParameters();
        Dictionary<string, object> GetParameters(int sequence);
        HttpParameters GetHttpParameters(int sequence, string[] parameter);
        SapParameters GetSapParameters();
    }
}
