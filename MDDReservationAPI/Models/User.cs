using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int Role { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [MaxLength(200)]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [MaxLength(200)]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }
    
    public bool? IsVerify { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
}