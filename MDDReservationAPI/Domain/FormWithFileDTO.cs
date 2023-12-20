namespace MDDReservationAPI.DTO;

public class FormWithFileDTO
{
    public ManagerCreationDTO managerCreationDto { get; set; }
    public SchoolCreationDTO schoolCreationDto { get; set; }
    public SchoolClassCreationDTO schoolClassCreationDto { get; set; }
    public bool hasProject { get; set; }
    public ProjectCreationDTO? projectCreationDto { get; set; }
    public ReservationSelectedDaysDTO reservationSelectedDays { get; set; }
    public int StudentListFileId { get; set; }
    public int ManagerFormFileId { get; set; }
    public FileUploadDTO StudentFile { get; set; }
    public FileUploadDTO ManagerFile { get; set; }
}