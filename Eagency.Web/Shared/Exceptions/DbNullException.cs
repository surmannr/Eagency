using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Web.Shared.Exceptions
{
    public class DbNullException : Exception
    {
        public DbNullException()
        {
        }

        public DbNullException(string message) : base(message)
        {
        }
    }
}
