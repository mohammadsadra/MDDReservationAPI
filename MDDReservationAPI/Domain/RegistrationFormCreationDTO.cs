using MDDReservationAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.DTO
{
    public class RegistrationFormCreationDTO
    {
        public int ManagerId { get; set; }

        public int SchoolId { get; set; }

        public int SchoolClassId { get; set; }

        public int? ProjectId { get; set; }
    }
}
