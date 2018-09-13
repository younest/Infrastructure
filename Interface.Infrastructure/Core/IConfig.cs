
using System.Collections.Generic;

using Interface.Infrastructure.Entities;

namespace Interface.Infrastructure.Core
{
    public interface IConfig
    {
        Dictionary<string, object> GetParameter();

        IParameter GetRequestParameters(int sequence, string[] parameter);
    }
}
