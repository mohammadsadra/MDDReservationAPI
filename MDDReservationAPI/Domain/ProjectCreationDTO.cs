using MDDReservationAPI.Models;

namespace MDDReservationAPI.DTO
{
    public class ProjectCreationDTO
    {
        public string? ProjectName { get; set; }

        public string? Description { get; set; }
        
        public int? SchoolId { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
