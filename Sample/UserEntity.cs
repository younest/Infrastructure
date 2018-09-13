using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    public enum ServiceName
    {
        UserService = 0,
        CustomerService = 1
    }
    public class UserEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
    }

    public class CustomerEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
