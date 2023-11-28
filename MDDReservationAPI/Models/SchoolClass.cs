using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.Models;

public class SchoolClass
{
    public SchoolClass()
    {
        this.CreatedAt = DateTime.UtcNow;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public int Grade { get; set; }
    
    [MaxLength(10)]
    public bool IsProgrammer { get; set; }
    
    [MaxLength(10)]
    public int ProgrammingLanguage { get; set; }

    // Relation
    [ForeignKey("SchoolId")]
    public int SchoolId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
}