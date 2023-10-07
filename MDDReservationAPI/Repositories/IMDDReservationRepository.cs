using MDDReservationAPI.DTO;
using MDDReservationAPI.Enums;
using MDDReservationAPI.Models;

namespace MDDReservationAPI.Repositories
{
    public interface IMDDReservationRepository
    {
        #region School

        Task<School> AddSchoolAsync(School school);
        Task<bool> SchoolExistsAsync(int schoolId);

        #endregion

        #region Manager

        Task<Manager> AddManagerAsync(Manager manager);

        #endregion

        #region Admin

        Task<Admin> AddAdminAsync(Admin admin);

        #endregion

        #region Student

        Task<Student> AddStudentAsync(Student student);

        #endregion

        #region Project

        Task<Project> AddProjectAsync(Project project);

        #endregion

        #region SchoolClass

        Task<SchoolClass> AddSchoolClassAsync(SchoolClass schoolClass);

        #endregion

        #region RegistrationForm

        Task<RegistrationForm> AddRegistrationFormAsync(RegistrationForm project);

        #endregion

        #region DB

        Task<bool> SaveChangesAsync();

        #endregion

        #region EventDays

        Task<EventDays> AddEventDaysAsync(EventDays eventDays);

        #endregion
       
        #region File
        public Task PostFileAsync(IFormFile fileData, FilePathType filePathType);
        

        public Task PostMultiFileAsync(List<FileUploadDTO> fileData);

        public Task DownloadFileById(int fileName);

        #endregion
    }
}
