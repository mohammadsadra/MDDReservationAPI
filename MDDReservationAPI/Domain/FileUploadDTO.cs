using MDDReservationAPI.Enums;

namespace MDDReservationAPI.DTO;

public class FileUploadDTO
{
    public IFormFile FileDetails { get; set; }
    public FilePathType FilePathType { get; set; }
}