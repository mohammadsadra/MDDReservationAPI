using MDDReservationAPI.Models;

namespace MDDReservationAPI.Repositories
{
    public interface IMDDReservationRepository
    {
        Task AddSchoolAsync(School school);
        Task<bool> SaveChangesAsync();
    }
}
