using FitnessMVC.Data;
using FitnessMVC.Models;
using FitnessMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessMVC.Controllers
{
    public class FeedBackController : Controller
    {

        ApplicationDbContext _mydbcontext;

        public FeedBackController(ApplicationDbContext mydbcontext)
        {
            _mydbcontext = mydbcontext;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            return View(_mydbcontext.FeedBacks.ToList());
        }

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            string userName = User.Identity.Name;

            // Filter feedbacks based on the user's name
            var userFeedbacks = _mydbcontext.FeedBacks
                .Where(feedback => feedback.Name == userName)
                .ToList();

            return View(userFeedbacks);
        }

        [Authorize]
        public IActionResult AddFeedBack()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult AddFeedBack(FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {

                // Save the feedback to the database
                _mydbcontext.FeedBacks.Add(feedBack);
                _mydbcontext.SaveChanges();

                ViewBag.Success = "Record Added";
                return RedirectToAction(nameof(Index));
            }

            // ModelState is not valid, return to the view with the validation errors
            return View(feedBack);
        }

        [Authorize(Roles = "User")]
        public IActionResult Edit(int id)
        {
            // Retrieve the feedback by its id
            var feedback = _mydbcontext.FeedBacks.Find(id);

            // Check if the feedback exists
            if (feedback == null)
            {
                return NotFound();
            }

            // Ensure that the user can only edit their own feedback
            if (feedback.Name != User.Identity.Name)
            {
                return Forbid(); // Or handle unauthorized access in a way that fits your application
            }

            return View(feedback);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Edit(int id, FeedBack updatedFeedback)
        {
            // Retrieve the feedback by its id
            var feedback = _mydbcontext.FeedBacks.Find(id);

            // Check if the feedback exists
            if (feedback == null)
            {
                return NotFound();
            }

            // Ensure that the user can only edit their own feedback
            if (feedback.Name != User.Identity.Name)
            {
                return Forbid(); // Or handle unauthorized access in a way that fits your application
            }

            // Update feedback properties with the new values
            feedback.Title = updatedFeedback.Title;
            feedback.Text = updatedFeedback.Text;

            // Save changes to the database
            _mydbcontext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "User")]
        public IActionResult Delete(int id)
        {
            // Retrieve the feedback by its id
            var feedback = _mydbcontext.FeedBacks.Find(id);

            // Check if the feedback exists
            if (feedback == null)
            {
                return NotFound();
            }

            // Ensure that the user can only delete their own feedback
            if (feedback.Name != User.Identity.Name)
            {
                return Forbid(); // Or handle unauthorized access in a way that fits your application
            }

            return View(feedback);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Retrieve the feedback by its id
            var feedback = _mydbcontext.FeedBacks.Find(id);

            // Check if the feedback exists
            if (feedback == null)
            {
                return NotFound();
            }

            // Ensure that the user can only delete their own feedback
            if (feedback.Name != User.Identity.Name)
            {
                return Forbid(); // Or handle unauthorized access in a way that fits your application
            }

            // Remove the feedback from the database
            _mydbcontext.FeedBacks.Remove(feedback);
            _mydbcontext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDelete(int id)
        {
            // Retrieve the feedback by its id
            var feedback = _mydbcontext.FeedBacks.Find(id);

            // Check if the feedback exists
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        [HttpPost, ActionName("AdminDelete")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminDeleteConfirmed(int id)
        {
            // Retrieve the feedback by its id
            var feedback = _mydbcontext.FeedBacks.Find(id);

            // Check if the feedback exists
            if (feedback == null)
            {
                return NotFound();
            }

            // Remove the feedback from the database
            _mydbcontext.FeedBacks.Remove(feedback);
            _mydbcontext.SaveChanges();

            return RedirectToAction(nameof(AdminIndex));
        }



    }
}
