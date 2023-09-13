using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MDDReservationAPI.Enums;
namespace MDDReservationAPI.Models;

public class Manager : User
{
    public Manager()
    {
        this.Role = (int) RoleEnum.Student;
        this.CreatedAt = DateTime.UtcNow;
    }
    
    [MaxLength(200)]
    public string Position { get; set; }
}