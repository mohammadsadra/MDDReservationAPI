using MDDReservationAPI.Models;

namespace MDDReservationAPI.Repositories
{
    public interface IMDDReservationRepository
    {
        #region School

        Task<School> AddSchoolAsync(School school);

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

        Task<RegistrationForm> AddProjectAsync(RegistrationForm project);

        #endregion

        #region DB

        Task<bool> SaveChangesAsync();

        #endregion
    }
}
