using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorWeb.Domain
{
    public interface IApplicationUser
    {
        public string? Name { get; }
        public string? Password { get; }
        public string? Email { get;  }
    }
}
