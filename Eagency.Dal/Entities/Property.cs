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
    public class Property
    {
        public int Id { get; set; }
        public Address Address { get; set; }
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
        public bool Sold { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Contract Contract { get; set; }

        [ForeignKey(nameof(User))]
        public string AgentId { get; set; }
        public User User { get; set; }
    }
}
