using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Group : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public int Capacity { get; set; }
        [Required]
        public int EducationId { get; set; }
        public Education Education { get; set; }
    }
}
