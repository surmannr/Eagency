using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Dal.Entities
{
    [Owned]
    public class Address
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Street { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(100)]
        public string Country { get; set; }
        [Required]
        public int ZipCode { get; set; }

        public int? PropertyId { get; set; }
    }
}
