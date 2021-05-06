using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Web.Shared.Enums
{
    public enum PaymentMethod
    {
        Cash = 1,
        Creditcard = 2,
        [Display(Name = "Bank transfer")]
        BankTransfer = 3
    }
}
