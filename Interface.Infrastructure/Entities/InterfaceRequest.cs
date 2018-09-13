using System.Collections.Generic;

namespace Interface.Infrastructure.Entities
{
    public class InterfaceRequest
    {
        public int Sequence { get; set; }

        public List<object> Parameter { get; set; }

        public InterfaceRequest()
        {
            Parameter = new List<object>();
        }
    }
}
