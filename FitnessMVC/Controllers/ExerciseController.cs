using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using FitnessMVC.Data;
using FitnessMVC.Interfaces;
using FitnessMVC.Models;
using FitnessMVC.ViewModels;

namespace FitnessMVC.Controllers
{
    public class ExerciseController : Controller
    {
        
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ExerciseController(IExerciseRepository exerciseRepository,IWebHostEnvironment hostEnvironment)
        {
            
            _exerciseRepository = exerciseRepository;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Exercise> exercises = await _exerciseRepository.GetAll();
            return View(exercises);
        }

        public async Task<IActionResult> Details(int id)
        {
            Exercise exercise = await _exerciseRepository.GetByIdAsync(id);
            return View(exercise);
        }

        public IActionResult Create()
        {
            return View();
        }

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


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var exerciseDetails = await _exerciseRepository.GetByIdAsync(id);
            if (exerciseDetails == null) return View("Error");
            return View(exerciseDetails);
        }

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
