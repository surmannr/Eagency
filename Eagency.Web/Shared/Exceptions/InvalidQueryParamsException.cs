using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Web.Shared.Exceptions
{
    public class InvalidQueryParamsException : Exception
    {
        public InvalidQueryParamsException()
        {
        }

        public InvalidQueryParamsException(string message) : base("Hibás paraméterek.")
        {
        }
    }
}
