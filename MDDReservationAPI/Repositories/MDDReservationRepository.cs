using MDDReservationAPI.Data;
using MDDReservationAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MDDReservationAPI.Repositories
{
    public class MDDReservationRepository : IMDDReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public MDDReservationRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }


        public async Task AddSchoolAsync(School school)
        {
            _context.Schools.Add(school);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
