using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MDDReservationAPI.Enums;

namespace MDDReservationAPI.Models;

public class FileDetails
{
    public FileDetails()
    {
        this.Guid = Guid.NewGuid();
        this.CreatedAt = DateTime.UtcNow;
    }
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public Guid Guid { get; set; }
    public string FileName { get; set; }
    public byte[] FileData { get; set; }
    
    public FilePathType FilePathType { get; set; }
    
    public FileKind FileKind { get; set; }
    
    // Relation
    [ForeignKey("RegistrationFormId")]
    public int? RegistrationFormId { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
}