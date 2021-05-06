using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Dal.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Question { get; set; }
        [StringLength(500)]
        public string Answer { get; set; }
        public DateTimeOffset Date { get; set; }

        [ForeignKey(nameof(Property))]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
