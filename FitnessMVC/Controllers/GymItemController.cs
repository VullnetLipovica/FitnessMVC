using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessMVC.Data;
using FitnessMVC.Interfaces;
using FitnessMVC.Models;
using FitnessMVC.Repository;
using FitnessMVC.ViewModels;

namespace FitnessMVC.Controllers
{
    public class GymItemController : Controller
    {
        private readonly IGymItemRepository _gymItemRepository;
        public GymItemController(IGymItemRepository gymItemRepository)
        {

            _gymItemRepository = gymItemRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<GymItem> gymItems = await _gymItemRepository.GetAll();
            return View(gymItems);
        }

        public async Task<IActionResult> Details(int id)
        {
            GymItem gymItems = await _gymItemRepository.GetByIdAsync(id);
            return View(gymItems);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GymItem gymItem)
        {
            if (!ModelState.IsValid)
            {
                return View(gymItem);
            }
            _gymItemRepository.Add(gymItem);
            return RedirectToAction("Index");
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var gymItem = await _gymItemRepository.GetByIdAsync(id);
			if (gymItem == null) return View("Error");
			var gymItemVM = new EditGymItemViewModel
			{
				Name = gymItem.Name,
				Description = gymItem.Description,
				Price = gymItem.Price,
			
			};
			return View(gymItemVM);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, EditGymItemViewModel GymItemVM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit club");
				return View("Edit", GymItemVM);
			}

			var gymItem = await _gymItemRepository.GetByIdAsyncNoTracking(id);

			if (gymItem == null)
			{
				return View("Error");
			}

			var newGymItem = new GymItem
			{
				GymItemId = id,
				Name = GymItemVM.Name,
				Description = GymItemVM.Description,
				Price = GymItemVM.Price,
				
			};

			_gymItemRepository.Update(newGymItem);

			return RedirectToAction("Index");
		}

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var GymItemDetails = await _gymItemRepository.GetByIdAsync(id);
            if (GymItemDetails == null) return View("Error");
            return View(GymItemDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var GymItemDetails = await _gymItemRepository.GetByIdAsync(id);

            if (GymItemDetails == null)
            {
                return View("Error");
            }

            _gymItemRepository.Delete(GymItemDetails);
            return RedirectToAction("Index");
        }
    }
}
