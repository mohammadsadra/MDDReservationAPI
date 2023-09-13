using MDDReservationAPI.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace MDDReservationAPI.DTO;

public class SchoolCreationDTO
{

    public string Name { get; set; }

    public int? SchoolType { get; set; }

    public int? Gender { get; set; }
}