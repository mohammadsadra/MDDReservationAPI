using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.Models;

public class Project
{
    public Project()
    {
        this.CreatedAt = DateTime.UtcNow;
    }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string ProjectName { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    
    // Relation
    [ForeignKey("SchoolId")]
    public int SchoolId { get; set; }
    
    
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
    
    public ICollection<Student>? Students { get; set; }
}