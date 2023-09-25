using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MDDReservationAPI.DTO
{
    public class ManagerCreationDTO
    {
        public string Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public bool? IsVerify { get; set; }

        public string? NationalId { get; set; }

        public string Position { get; set; }
    }
}
