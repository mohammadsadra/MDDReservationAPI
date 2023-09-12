using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.Models;

public class RegistrationForm
{
    
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public Manager Manager { get; set; }
    
    [Required]
    public School School { get; set; }
    
    [Required]
    public SchoolClass SchoolClass { get; set; }
    

    public Project? Project { get; set; }
}