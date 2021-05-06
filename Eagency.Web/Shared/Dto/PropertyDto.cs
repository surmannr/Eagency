using Eagency.Web.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Web.Shared.Dto
{
    public class PropertyDto
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

        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int NumberOfGarages { get; set; }
        public int NumberOfParkingSpaces { get; set; }
        [StringLength(3000)]
        public string Description { get; set; }
        public HouseType HouseType { get; set; }
        public bool IsFurnished { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Area { get; set; }
        public string ImageName { get; set; }

        public int? ContractId { get; set; }
        public string AgentId { get; set; }
    }
}
