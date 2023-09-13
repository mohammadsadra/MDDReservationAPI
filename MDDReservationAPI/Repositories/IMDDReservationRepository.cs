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

        Task<bool> SaveChangesAsync();
    }
}
