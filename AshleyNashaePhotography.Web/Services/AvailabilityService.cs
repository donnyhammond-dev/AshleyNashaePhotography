using AshleyNashaePhotography.Web.Data;
using Microsoft.EntityFrameworkCore;
using AshleyNashaePhotography.Web.DTOs;

namespace AshleyNashaePhotography.Web.Services
{
    public class AvailabilityService
    {
        private readonly ApplicationDbContext _context;

        public AvailabilityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DateOnly>> GetUnavailableDatesAsync()
        {
            var sessionDates = await _context.PhotoSessions
                .Select(x => x.Date)
                .ToListAsync();

            var blockedDates = await _context.BlockedDates
                .Select(x => x.Date)
                .ToListAsync();

            return sessionDates.Concat(blockedDates).OrderBy(x => x).ToList();
        }

        public async Task<AvailabilityDetailsDto> GetAvailabilityDetailsAsync()
        {
            return new AvailabilityDetailsDto
            {
                BlockedDates = await _context.BlockedDates
                .Select(x => x.Date)
                .OrderBy(x => x)
                .ToListAsync(),

                BookedDates = await _context.PhotoSessions
                .Select(x => x.Date)
                .OrderBy(x => x)
                .ToListAsync()
            };
        }
    }
}
