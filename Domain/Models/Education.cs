using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Models
{
    public class Education : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Color { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Group> Groups { get; set; }
    }
}
