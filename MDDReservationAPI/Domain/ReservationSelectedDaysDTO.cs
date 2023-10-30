namespace MDDReservationAPI.DTO;

public class ReservationSelectedDaysDTO
{
    public bool? IsSelected { get; set; }
    
    public int RegestrationFormId { get; set; }
    
    public int EventDaysId { get; set; }
    
    public int TeamId { get; set; }
    
    public int Year { get; set; }
    
    public int Month { get; set; }
}