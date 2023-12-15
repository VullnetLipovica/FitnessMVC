using FitnessMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessMVC.Data.Enum;
using FitnessMVC.Interfaces;
using FitnessMVC.Models;

namespace FitnessMVC.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ExerciseRepository(ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

      /*  private void SaveImageFile([Bind("exName,exDescription,BodyParts,Difficulty,ImageFile")] Exercise exercise)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(exercise.ImageFile.FileName);
            string extension = Path.GetExtension(exercise.ImageFile.FileName);
            exercise.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Image/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                exercise.ImageFile.CopyTo(fileStream);
            }
        }*/

        public bool Add(Exercise exercise)
        {
           
            _context.Add(exercise);
          
            /* SaveImageFile(exercise);*/
           
            return Save();
        }
       

        public bool Delete(Exercise exercise)
        {
            _context.Remove(exercise);
            return Save();
        }

        public async Task<IEnumerable<Exercise>> GetAll()
        {
          return await _context.Exercises.ToListAsync();

        }

        public IQueryable<Exercise> GetAllNew()
        {
            return _context.Exercises.AsQueryable();
        }

        public async Task<Exercise> GetByIdAsync(int id)
        {
            return await _context.Exercises.FirstOrDefaultAsync(i => i.ExerciseId == id);
        }

        public async Task<Exercise> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Exercises.AsNoTracking().FirstOrDefaultAsync(i => i.ExerciseId == id);
        }
        public Task<IEnumerable<Exercise>> GetExerciseByBodyPart(Enum bodyPart)
        {
            throw new NotImplementedException();
        }

        //duhet mu ndreq
        /*  public async Task<IEnumerable<Exercise>> GetExerciseByBodyPart(BodyParts bodyPart)
          {

              ///return await _context.Exercises.Where(b => b.BodyParts.HasFlag(bodyPart));
          }
        */

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Exercise exercise)
        {
            _context.Update(exercise);
            return Save();
        }
    }
}
