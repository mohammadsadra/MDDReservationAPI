using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.Models;

public class School
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(10)]
    public int? SchoolType { get; set; }

    [MaxLength(2)]
    public int? Gender { get; set; }
    
    [DataType(DataType.DateTime)]
    public string CreatedAt { get; set; }
    
    public ICollection<SchoolClass>? SchoolClasses { get; set; }
}