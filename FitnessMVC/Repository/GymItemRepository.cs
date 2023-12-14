using Microsoft.EntityFrameworkCore;
using System;
using FitnessMVC.Data;
using FitnessMVC.Interfaces;
using FitnessMVC.Models;

namespace FitnessMVC.Repository
{
    public class GymItemRepository : IGymItemRepository
    {
        private readonly ApplicationDbContext _context;
        public GymItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(GymItem gymItem)
        {
            _context.Add(gymItem);
            return Save();
        }

        public bool Delete(GymItem gymItem)
        {
            _context.Remove(gymItem);
            return Save();
        }

        public async Task<IEnumerable<GymItem>> GetAll()
        {
            return await _context.GymItems.ToListAsync();
        }

        public async Task<GymItem> GetByIdAsync(int id)
        {
            return await _context.GymItems.FirstOrDefaultAsync(i => i.GymItemId == id);
        }

		public async Task<GymItem> GetByIdAsyncNoTracking(int id)
		{
			return await _context.GymItems.AsNoTracking().FirstOrDefaultAsync(i => i.GymItemId == id);
		}

		public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(GymItem gymItem)
        {
            _context.Update(gymItem);
            return Save();
        }
    }
}
