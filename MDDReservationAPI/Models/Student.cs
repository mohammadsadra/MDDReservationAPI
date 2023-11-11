using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MDDReservationAPI.Enums;

namespace MDDReservationAPI.Models;

public class Student : User
{
    public Student()
    {
        this.Role = (int) RoleEnum.Student;;
        this.CreatedAt = DateTime.UtcNow;
    }
    
    // Relation
    [ForeignKey("SchoolClassId")]
    public int SchoolClassId { get; set; }
}