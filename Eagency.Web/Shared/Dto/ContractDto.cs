using Eagency.Web.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Web.Shared.Dto
{
    public class ContractDto
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        [Range(0, 1)]
        public double FeePercentage { get; set; }
        [Required]
        public int PaymentFrequency { get; set; }
        public bool IsPaid { get; set; }
        public bool IsSigned { get; set; }

        public int PropertyId { get; set; }

        public string AgentName { get; set; }

        public string ClientId { get; set; }
        public string ClientName { get; set; }
    }
}
