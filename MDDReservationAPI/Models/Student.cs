using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MDDReservationAPI.Enums;

namespace MDDReservationAPI.Models;

public class Student : User
{
    public Student()
    {
        this.Role = (int) RoleEnum.Student;;
    }
    
    // Relation
    [ForeignKey("SchoolClassId")]
    public SchoolClass? SchoolClass { get; set; }
    public int SchoolClassId { get; set; }
}