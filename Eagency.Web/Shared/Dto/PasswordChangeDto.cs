using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Web.Shared.Dto
{
    public class PasswordChangeDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
