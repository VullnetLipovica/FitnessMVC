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
        public async Task<IActionResult> Index()
        {
            IEnumerable<Membership> memberships= await _membershipRepository.GetAll();
            return View(memberships);
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
