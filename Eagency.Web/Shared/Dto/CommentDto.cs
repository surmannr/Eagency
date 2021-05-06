using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Web.Shared.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Question { get; set; }
        [StringLength(500)]
        public string Answer { get; set; }

        public int PropertyId { get; set; }

        public string UserId { get; set; }
    }
}
