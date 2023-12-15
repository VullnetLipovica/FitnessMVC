using FitnessMVC.Models;
using FitnessMVC.ViewModels;

namespace FitnessMVC.Interfaces
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAll();

        IQueryable<Exercise> GetAllNew();
        Task<Exercise> GetByIdAsync(int id);
        Task<Exercise> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Exercise>> GetExerciseByBodyPart(Enum bodyPart);
        bool Add(Exercise exercise);
        bool Delete(Exercise exercise);

        bool Update(Exercise exercise);
        bool Save();
        
    }
}
