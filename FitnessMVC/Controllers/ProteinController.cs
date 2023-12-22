using FitnessMVC.Data;
using FitnessMVC.Models;
using FitnessMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FitnessMVC.Controllers
{
    public class ProteinController : Controller
    {
        ApplicationDbContext _mydbcontext;
        IWebHostEnvironment _webEnvironment;
        public ProteinController(ApplicationDbContext mydbcontext, IWebHostEnvironment webHostEnvironment)
        {
            _mydbcontext = mydbcontext;
            _webEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_mydbcontext.Proteins.ToList());
        }

        public IActionResult AddProtein()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProtein(ProteinViewModel protein)
        {
            String filename = "";
            if(protein.photo!=null)
            {
                string uploadfolder = Path.Combine(_webEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + protein.photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                protein.photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }

            Protein p = new Protein
            {
                Name = protein.Name,
                Description = protein.Description,
                Price = protein.Price,
                Image = filename
            };
            _mydbcontext.Proteins.Add(p);
            _mydbcontext.SaveChanges();
            ViewBag.succes = "Record Added";
            return View();
        }
    }
}
