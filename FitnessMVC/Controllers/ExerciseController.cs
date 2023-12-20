using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using FitnessMVC.Data;
using FitnessMVC.Interfaces;
using FitnessMVC.Models;
using FitnessMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FitnessMVC.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class ExerciseController : Controller
    {
        
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ExerciseController(IExerciseRepository exerciseRepository,IWebHostEnvironment hostEnvironment)
        {
            
            _exerciseRepository = exerciseRepository;
            _hostEnvironment = hostEnvironment;
        }

		
		public async Task<IActionResult> Index(string searchString, string sortOrder,int pageNumber, string currentFilter)
        {
            IEnumerable<Exercise> exercises = await _exerciseRepository.GetAll();
            var exercises1 = _exerciseRepository.GetAllNew();
          
            //Search
            if (!String.IsNullOrEmpty(searchString))
            {
                exercises = exercises.Where(n => n.exName.ToLower().Trim().Contains(searchString.ToLower().Trim()));
                exercises1 = exercises1.Where(n => n.exName.ToLower().Trim().Contains(searchString.ToLower().Trim()));
            }

           
            //Sort
            switch (sortOrder)
            {
                case "name_desc":
                    exercises = exercises.OrderByDescending(e => e.exName).ToList();
                    exercises1 = exercises1.OrderByDescending(e => e.exName);
                    break;
                case "difficulty_asc":
                    exercises = exercises.OrderBy(e => e.Difficulty).ToList();
                    exercises1 = exercises1.OrderBy(e => e.Difficulty);
                    break;
                case "difficulty_desc":
                    exercises = exercises.OrderByDescending(e => e.Difficulty).ToList();
                    exercises1 = exercises1.OrderByDescending(e => e.Difficulty);
                    break;

                default:
                    exercises = exercises.OrderBy(e => e.exName).ToList();
                    exercises1 = exercises1.OrderBy(e => e.exName);
                    break;
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            int pageSize = 5;
            return View(await PaginatedList<Exercise>.CreateAsync(exercises1, pageNumber, pageSize));
        }
		
		public async Task<IActionResult> Details(int id)
        {
            Exercise exercise = await _exerciseRepository.GetByIdAsync(id);
            return View(exercise);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]

        public async Task<IActionResult> Create(Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return View(exercise);
            }
         
            _exerciseRepository.Add(exercise);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(id);
            if(exercise == null) return View("Error");
            var exerciseVM = new EditExerciseViewModel
            {
                exName = exercise.exName,
                exDescription = exercise.exDescription,
                BodyParts = exercise.BodyParts,
                Difficulty = exercise.Difficulty
            };
            return View(exerciseVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditExerciseViewModel ExerciseVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", ExerciseVM);
            }

            var exercise = await _exerciseRepository.GetByIdAsyncNoTracking(id);

            if (exercise == null)
            {
                return View("Error");
            }

            var newExercise = new Exercise
            {
                ExerciseId = id,
                exName = ExerciseVM.exName,
                exDescription = ExerciseVM.exDescription,
                BodyParts = ExerciseVM.BodyParts,
                Difficulty = ExerciseVM.Difficulty
            };

            _exerciseRepository.Update(newExercise);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var exerciseDetails = await _exerciseRepository.GetByIdAsync(id);
            if (exerciseDetails == null) return View("Error");
            return View(exerciseDetails);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exerciseDetails = await _exerciseRepository.GetByIdAsync(id);

            if (exerciseDetails == null)
            {
                return View("Error");
            }

            _exerciseRepository.Delete(exerciseDetails);
            return RedirectToAction("Index");
        }
    }
}
