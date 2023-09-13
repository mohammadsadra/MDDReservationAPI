using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.Models;

public class RegistrationForm
{
    public RegistrationForm()
    {
        this.CreatedAt = DateTime.UtcNow;
    }
    
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // Relation
    [ForeignKey("ManagerId")]
    public Manager Manager { get; set; }
    public int ManagerId {get; set;}

    // Relation
    [ForeignKey("SchoolId")]
    public School School { get; set; }
    public int SchoolId { get; set; }

    // Relation
    [ForeignKey("SchoolClassId")]
    public SchoolClass SchoolClass { get; set; }
    public int SchoolClassId { get; set; }

    // Relation
    [ForeignKey("ProjectId")]
    public Project? Project { get; set; }
    public int? ProjectId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
}