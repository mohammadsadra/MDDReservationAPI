using MDDReservationAPI.Enums;

namespace MDDReservationAPI.DTO;

public class FileUploadDTO
{
    public Guid guid { get; set; }
    public IFormFile FileDetails { get; set; }
    public FilePathType FilePathType { get; set; }
    public FileKind FileKind { get; set; }
    public int? RegistrationFormId { get; set; }
}