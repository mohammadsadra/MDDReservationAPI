using MDDReservationAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MDDReservationAPI.DTO
{
    public class SchoolClassCreationDTO
    {
        public int Grade { get; set; }

        public bool IsProgrammer { get; set; }

        public int ProgrammingLanguage { get; set; }

        public int? SchoolId { get; set; }
    }
}
