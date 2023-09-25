namespace MDDReservationAPI.DTO;

public class EventDaysCreationDTO
{
    public bool? Saturday { get; set; } = false;
    public bool? Sunday { get; set; } = false;
    public bool? Monday { get; set; } = false;
    public bool? Tuesday { get; set; } = false;
    public bool? Wednesday { get; set; } = false;
    public bool? Thursday { get; set; } = false;
    public bool? Friday { get; set; } = false;
}