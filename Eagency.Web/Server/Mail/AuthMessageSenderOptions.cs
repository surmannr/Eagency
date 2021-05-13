using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eagency.Web.Server.Mail
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
