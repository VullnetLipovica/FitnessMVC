using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FitnessMVC.Data;
using FitnessMVC.Interfaces;
using FitnessMVC.Models;
using FitnessMVC.Repository;
using FitnessMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FitnessMVC.Controllers
{
	[Authorize(Roles = "User,Admin")]
	public class MembershipController : Controller
    {
        private readonly IMembershipRepository _membershipRepository;

        public MembershipController(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }
        public async Task<IActionResult> Index(string searchString, string sortOrder, int pageNumber, string currentFilter)
        {
            IEnumerable<Membership> memberships = await _membershipRepository.GetAll();
            var memberships1 = _membershipRepository.GetAllNew();

            //Search
            if (!String.IsNullOrEmpty(searchString))
            {
                memberships = memberships.Where(n => n.memName.ToLower().Trim().Contains(searchString.ToLower().Trim()));
                memberships1 = memberships1.Where(n => n.memName.ToLower().Trim().Contains(searchString.ToLower().Trim()));
            }


            //Sort
            switch (sortOrder)
            {
                case "name_desc":
                    memberships = memberships.OrderByDescending(e => e.memName).ToList();
                    memberships1 = memberships1.OrderByDescending(e => e.memName);
                    break;
                case "length_asc":
                    memberships = memberships.OrderBy(e => e.memLength).ToList();
                    memberships1 = memberships1.OrderBy(e => e.memLength);
                    break;
                case "length_desc":
                    memberships = memberships.OrderByDescending(e => e.memLength).ToList();
                    memberships1 = memberships1.OrderByDescending(e => e.memLength);
                    break;

                default:
                    memberships = memberships.OrderBy(e => e.memName).ToList();
                    memberships1 = memberships1.OrderBy(e => e.memName);
                    break;
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            int pageSize = 5;
            return View(await PaginatedList<Membership>.CreateAsync(memberships1, pageNumber, pageSize));
        }

        public async Task<IActionResult> Details(int id)
        {
            Membership membership = await _membershipRepository.GetByIdAsync(id);
            return View(membership);
        }

		[Authorize(Roles = "Admin")]

		public IActionResult Create()
        {
            return View();
        }

		[Authorize(Roles = "Admin")]
		[HttpPost]

        public async Task<IActionResult> Create(Membership membership)
        {
            if (!ModelState.IsValid)
            {
                return View(membership);
            }
            _membershipRepository.Add(membership);
            return RedirectToAction("Index");
        }

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var membership = await _membershipRepository.GetByIdAsync(id);
            if (membership == null) return View("Error");
            var membershipVM = new EditMembershipViewModel
            {
                memName = membership.memName,
                memLength = membership.memLength,
            };
            return View(membershipVM);
        }
		
        [Authorize(Roles = "Admin")]
		[HttpPost]
        public async Task<IActionResult> Edit(int id, EditMembershipViewModel membershipVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", membershipVM);
            }

            var membership = await _membershipRepository.GetByIdAsyncNoTracking(id);

            if (membership == null)
            {
                return View("Error");
            }

            var newMembership = new Membership
            {
                memId = id,
                memName = membershipVM.memName,
                memLength = membershipVM.memLength,
            };

            _membershipRepository.Update(newMembership);

            return RedirectToAction("Index");
        }

		[Authorize(Roles = "Admin")]
		[HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var membershipDetails = await _membershipRepository.GetByIdAsync(id);
            if (membershipDetails == null) return View("Error");
            return View(membershipDetails);
        }

		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var membershipDetails = await _membershipRepository.GetByIdAsync(id);

            if (membershipDetails == null)
            {
                return View("Error");
            }

            _membershipRepository.Delete(membershipDetails);
            return RedirectToAction("Index");
        }

    }
}
