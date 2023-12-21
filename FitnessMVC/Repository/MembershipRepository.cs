using FitnessMVC.Data;
using Microsoft.EntityFrameworkCore;
using FitnessMVC.Interfaces;
using FitnessMVC.Models;

namespace FitnessMVC.Repository
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly ApplicationDbContext _context;

        public MembershipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Membership membership)
        {
            _context.Add(membership);
            return Save();
        }

        public bool Delete(Membership membership)
        {
            _context.Remove(membership);
            return Save();
        }

        public async Task<IEnumerable<Membership>> GetAll()
        {
            return await _context.Memberships.ToListAsync();
        }
        public IQueryable<Membership> GetAllNew()
        {
            return _context.Memberships.AsQueryable();
        }

        public async Task<Membership> GetByIdAsync(int id)
        {
            return await _context.Memberships.FirstOrDefaultAsync(i => i.memId == id);
        }

        public async Task<Membership> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Memberships.AsNoTracking().FirstOrDefaultAsync(i => i.memId == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Membership membership)
        {
            _context.Update(membership);
            return Save();
        }
    }
}
