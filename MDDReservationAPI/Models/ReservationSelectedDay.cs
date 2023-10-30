using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.Models;

public class ReservationSelectedDay
{
    public ReservationSelectedDay()
    {
        this.CreatedAt = DateTime.UtcNow;
        this.IsVerify = false;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public bool IsVerify { get; set; }
    
    public bool IsSelected { get; set; }
    
    public int Year { get; set; }
    
    public int Month { get; set; }

    // Relation
    [ForeignKey("RegestrationFormId")]
    public int RegestrationFormId { get; set; }
    
    [ForeignKey("EventDaysId")]
    public int EventDaysId { get; set; }
    
    [ForeignKey("TeamId")]
    public int TeamId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
}