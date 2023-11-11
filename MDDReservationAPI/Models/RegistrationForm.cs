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
    public int ManagerId {get; set;}

    // Relation
    [ForeignKey("SchoolId")]
    public int SchoolId { get; set; }

    // Relation
    [ForeignKey("SchoolClassId")]
    public int SchoolClassId { get; set; }

    // Relation
    [ForeignKey("ProjectId")]
    public int? ProjectId { get; set; }
    
    // Relation
    [ForeignKey("ReservationSelectedDaysId")]
    public int ReservationSelectedDaysId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
}