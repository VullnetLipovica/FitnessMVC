using FitnessMVC.Models;

namespace FitnessMVC.Interfaces
{
    public interface IMembershipRepository
    {
        Task<IEnumerable<Membership>> GetAll();
        Task<Membership> GetByIdAsync(int id);

        Task<Membership> GetByIdAsyncNoTracking(int id);
        bool Add(Membership membership);
        bool Delete(Membership membership);

        bool Update(Membership membership);
        bool Save();
    }
}
