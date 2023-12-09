using MDDReservationAPI.DTO;
using MDDReservationAPI.Enums;
using MDDReservationAPI.Models;

namespace MDDReservationAPI.Repositories
{
    public interface IMDDReservationRepository
    {
        #region School

        Task<School> AddSchoolAsync(School school);
        
        Task<School?> GetSchoolByIdAsync(int id);
        
        Task<bool> SchoolExistsAsync(int schoolId);

        #endregion

        #region Manager

        Task<Manager> AddManagerAsync(Manager manager);
        
        Task<Manager?> GetManagerByIdAsync(int id);

        #endregion

        #region Admin

        Task<Admin> AddAdminAsync(Admin admin);
        
        Task<Admin?> GetAdminByIdAsync(int id);

        #endregion

        #region Student

        Task<Student> AddStudentAsync(Student student);
        
        Task<Student?> GetStudentByIdAsync(int id);

        #endregion

        #region Project

        Task<Project> AddProjectAsync(Project project);
        
        Task<Project?> GetProjectByIdAsync(int id);

        #endregion

        #region SchoolClass

        Task<SchoolClass> AddSchoolClassAsync(SchoolClass schoolClass);

        Task<SchoolClass?> GetSchoolClassByIdAsync(int id);

        #endregion

        #region RegistrationForm

        Task<RegistrationForm> AddRegistrationFormAsync(RegistrationForm project);

        Task<RegistrationForm?> GetRegistrationFormByIdAsync(int id);

        Task<string> CreatePdfFromRegistrationFormId(int id);

        Task<string> CreateExcelFromRegistrationFormId(int id);

        #endregion

        #region DB

        Task<bool> SaveChangesAsync();

        #endregion
        
       
        #region File
        public Task<int> PostFileAsync(FileUploadDTO fileDetails);
        

        public Task PostMultiFileAsync(List<FileUploadDTO> fileData);

        public Task DownloadFileById(Guid guid);

        public Task<bool> ChangeRegisterFormId(int fileId,int registerFormId);
        #endregion
        
        #region ReservationDays

        public Task<ReservationSelectedDay> AddSelectedDays(ReservationSelectedDay reservationSelectedDay);
        
        public Task<ReservationSelectedDay?> GetSelectedDaysByReservationId(int reservationId);

        #endregion
    }
}
