using Eagency.Web.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Dal.Entities
{
    public class Contract
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        [Range(0,1)]
        public double FeePercentage { get; set; }
        [Required]
        public int PaymentFrequency { get; set; }
        public bool IsPaid { get; set; }
        public bool IsSigned { get; set; }

        [ForeignKey(nameof(Property))]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        [ForeignKey(nameof(User))]
        public string ClientId { get; set; }
        public User User { get; set; }
    }
}
