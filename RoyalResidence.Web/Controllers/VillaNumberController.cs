using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoyalResidence.Domain.Entities;
using RoyalResidence.Infrastructure.Data;
using RoyalResidence.Web.ViewModels;

namespace RoyalResidence.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberController (ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.Include(u => u.Villa).ToList();
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM Obj)
        {
            bool villaNumberExists = _db.VillaNumbers.Any(u => u.Villa_Number == Obj.VillaNumber.Villa_Number);
            if (ModelState.IsValid && !villaNumberExists)
            {
                _db.VillaNumbers.Add(Obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been created successfully.";
                return RedirectToAction("Index", "VillaNumber");
            }
            if (villaNumberExists)
            {
                TempData["error"] = "The villa number already exists.";
            }
            Obj.VillaList = _db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(Obj);
        }
    }
}
