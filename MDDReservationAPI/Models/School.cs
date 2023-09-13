using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.Models;

public class School
{
    public School()
    {
        this.CreatedAt = DateTime.UtcNow;
    }

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

    // Relation
    [ForeignKey("ManagerId")]
    public Manager? manager { get; set; }
    public int ManagerId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
    
    public ICollection<SchoolClass>? SchoolClasses { get; set; }
}