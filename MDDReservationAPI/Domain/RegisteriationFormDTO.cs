using MDDReservationAPI.Models;

namespace MDDReservationAPI.DTO;

public class RegisteriationFormDTO
{
    public int Id { get; set; }
    
    public int ManagerId {get; set;}
    
    public Manager? Manager { get; set; }
    
    public int SchoolId { get; set; }
    
    public School? School { get; set; }
    
    public int SchoolClassId { get; set; }
    
    public SchoolClass? SchoolClass { get; set; }
    
    public int? ProjectId { get; set; }
  
    public int ReservationSelectedDaysId { get; set; }
    
    public ReservationSelectedDay? ReservationSelectedDays { get; set; }

    public DateTime CreatedAt { get; set; }
}