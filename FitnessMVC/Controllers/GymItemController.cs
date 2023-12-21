using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessMVC.Data;
using FitnessMVC.Interfaces;
using FitnessMVC.Models;
using FitnessMVC.Repository;
using FitnessMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FitnessMVC.Controllers
{
	[Authorize(Roles = "User,Admin")]
	public class GymItemController : Controller
    {
        private readonly IGymItemRepository _gymItemRepository;
        public GymItemController(IGymItemRepository gymItemRepository)
        {

            _gymItemRepository = gymItemRepository;
        }
        public async Task<IActionResult> Index(string searchString, string sortOrder, int pageNumber, string currentFilter)
        {
            IEnumerable<GymItem> gymItems = await _gymItemRepository.GetAll();
            var gymitems1 = _gymItemRepository.GetAllNew();

            //Search
            if (!String.IsNullOrEmpty(searchString))
            {
                gymItems = gymItems.Where(n => n.Name.ToLower().Trim().Contains(searchString.ToLower().Trim()));
                gymitems1 = gymitems1.Where(n => n.Name.ToLower().Trim().Contains(searchString.ToLower().Trim()));
            }


            //Sort
            switch (sortOrder)
            {
                case "name_desc":
                    gymItems = gymItems.OrderByDescending(e => e.Name).ToList();
                    gymitems1 = gymitems1.OrderByDescending(e => e.Name);
                    break;
                case "price_asc":
                    gymItems = gymItems.OrderBy(e => e.Price).ToList();
                    gymitems1 = gymitems1.OrderBy(e => e.Price);
                    break;
                case "price_desc":
                    gymItems = gymItems.OrderByDescending(e => e.Price).ToList();
                    gymitems1 = gymitems1.OrderByDescending(e => e.Price);
                    break;

                default:
                    gymItems = gymItems.OrderBy(e => e.Name).ToList();
                    gymitems1 = gymitems1.OrderBy(e => e.Name);
                    break;
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            int pageSize = 5;
            return View(await PaginatedList<GymItem>.CreateAsync(gymitems1, pageNumber, pageSize));
        }


        public async Task<IActionResult> Details(int id)
        {
            GymItem gymItems = await _gymItemRepository.GetByIdAsync(id);
            return View(gymItems);
        }

		[Authorize(Roles = "Admin")]
		public IActionResult Create()
        {
            return View();
        }

		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var GymItemDetails = await _gymItemRepository.GetByIdAsync(id);
            if (GymItemDetails == null) return View("Error");
            return View(GymItemDetails);
        }

		[Authorize(Roles = "Admin")]
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
