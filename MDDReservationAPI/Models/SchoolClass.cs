using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDDReservationAPI.Models;

public class SchoolClass
{
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

    [MaxLength(2)]
    public int? Gender { get; set; }
    
    [DataType(DataType.DateTime)]
    public string CreatedAt { get; set; }
    
    public ICollection<Student>? Students { get; set; }
}