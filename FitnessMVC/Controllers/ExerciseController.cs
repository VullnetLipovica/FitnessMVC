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
        private readonly IWebHostEnvironment _webEnvironment;
        ApplicationDbContext _mydbcontext;

        public ExerciseController(IExerciseRepository exerciseRepository,IWebHostEnvironment webHostEnvironment, ApplicationDbContext mydbcontext)
        {
            
            _exerciseRepository = exerciseRepository;
            _webEnvironment = webHostEnvironment;
            _mydbcontext = mydbcontext;
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

        public async Task<IActionResult> Create(ExerciseViewModel exercise)
        {
            String filename = "";
            if (exercise.photo != null)
            {
                string uploadfolder = Path.Combine(_webEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + exercise.photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                exercise.photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }

            Exercise e = new Exercise
            {
                exName = exercise.exName,
                exDescription = exercise.exDescription,
                Difficulty = exercise.Difficulty,
                BodyParts = exercise.BodyParts,
                Image = filename
            };

            _mydbcontext.Exercises.Add(e);
            _mydbcontext.SaveChanges();
            ViewBag.succes = "Record Added";
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
                Difficulty = exercise.Difficulty,
            };
            return View(exerciseVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditExerciseViewModel ExerciseVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit exercise");
                return View("Edit", ExerciseVM);
            }

            var exercise = await _exerciseRepository.GetByIdAsyncNoTracking(id);

            if (exercise == null)
            {
                return View("Error");
            }

            // Update existing properties
            exercise.exName = ExerciseVM.exName;
            exercise.exDescription = ExerciseVM.exDescription;
            exercise.BodyParts = ExerciseVM.BodyParts;
            exercise.Difficulty = ExerciseVM.Difficulty;

            // Handle the new image
            if (ExerciseVM.NewImage != null)
            {
                // Delete the existing image if it exists
                if (!string.IsNullOrEmpty(exercise.Image))
                {
                    string existingImagePath = Path.Combine(_webEnvironment.WebRootPath, "images", exercise.Image);
                    if (System.IO.File.Exists(existingImagePath))
                    {
                        System.IO.File.Delete(existingImagePath);
                    }
                }

                // Save the new image
                string newImageFilename = Guid.NewGuid().ToString() + "_" + ExerciseVM.NewImage.FileName;
                string newImagePath = Path.Combine(_webEnvironment.WebRootPath, "images", newImageFilename);
                ExerciseVM.NewImage.CopyTo(new FileStream(newImagePath, FileMode.Create));

                // Update the exercise with the new image filename
                exercise.Image = newImageFilename;
            }

            _exerciseRepository.Update(exercise);

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
