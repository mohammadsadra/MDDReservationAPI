using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MDDReservationAPI.Enums;

namespace MDDReservationAPI.Models;

public class FileDetails
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string FileName { get; set; }
    public byte[] FileData { get; set; }
    
    public FilePathType FilePathType { get; set; }
    
    public FileKind FilePathKind { get; set; }
}