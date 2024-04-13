namespace Service.DTOs.Groups
{
    public class GroupUpdateDTo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int EducationId { get; set; }
    }
}
