using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.Models;

public class ReservationSelectedDay
{
    public ReservationSelectedDay()
    {
        this.CreatedAt = DateTime.UtcNow;
        this.IsVerify = false;
        this.SelectedDay = 0;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public bool IsVerify { get; set; }
    public int SelectedDay { get; set; }
    
    public DateTime FirstDay { get; set; }
    
    public DateTime SecondDay { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
}