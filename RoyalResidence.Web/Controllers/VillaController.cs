using Microsoft.AspNetCore.Mvc;
using RoyalResidence.Domain.Entities;
using RoyalResidence.Infrastructure.Data;

namespace RoyalResidence.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa Obj)
        {
            if (Obj.Name == Obj.Description)
            {
                ModelState.AddModelError("Name", "Name and Description cannot be the same");
                ModelState.AddModelError("Description", "Name and Description cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Villas.Add(Obj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been created successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }
        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if(obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Villa Obj)
        {
            if(ModelState.IsValid)
            {
                _db.Villas.Update(Obj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been updated successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }
        public IActionResult Delete(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa Obj)
        {
            Villa? objFromDb = _db.Villas.FirstOrDefault(u => u.Id == Obj.Id); 
            if (objFromDb is not null)
            {
                _db.Villas.Remove(objFromDb);
                _db.SaveChanges();
                TempData["success"] = "The villa has been deleted successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }
    }
}
