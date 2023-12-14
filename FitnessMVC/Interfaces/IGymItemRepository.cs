using FitnessMVC.Models;

namespace FitnessMVC.Interfaces
{
    public interface IGymItemRepository
    {
        Task<IEnumerable<GymItem>> GetAll();
        Task<GymItem> GetByIdAsync(int id);

		Task<GymItem> GetByIdAsyncNoTracking(int id);
		bool Add(GymItem gymItem);
        bool Delete(GymItem gymItem);

        bool Update(GymItem gymItem);
        bool Save();
    }
}
