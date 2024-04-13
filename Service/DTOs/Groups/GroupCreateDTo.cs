using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Groups
{
    public class GroupCreateDTo
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int EducationId { get; set; }
    }
}
