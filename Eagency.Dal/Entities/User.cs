using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Dal.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public ICollection<Contract> Contracts { get; set; }
        public ICollection<Property> Properties { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
