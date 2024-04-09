using Domain.Common;

namespace Domain.Models
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; set; }
        public int EducationId { get; set; }
        public Education Education { get; set; }
    }
}
