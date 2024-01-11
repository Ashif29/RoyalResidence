using Microsoft.AspNetCore.Mvc;
using RoyalResidence.Application.Common.Interfaces;
using RoyalResidence.Domain.Entities;
using RoyalResidence.Infrastructure.Data;

namespace RoyalResidence.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();
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
                if(Obj.Image is not null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImage");

                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    Obj.Image.CopyTo(fileStream);

                    Obj.ImageUrl = @"\images\VillaImage\" + fileName;
                }
                else
                {
                    Obj.ImageUrl = "https://placehold.co/600x400";
                }
                _unitOfWork.Villa.Add(Obj);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been created successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }
        public IActionResult Update(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
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
                if (Obj.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\VillaImage");

                    if (!string.IsNullOrEmpty(Obj.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, Obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    Obj.Image.CopyTo(fileStream);

                    Obj.ImageUrl = @"\images\VillaImage\" + fileName;
                }
                _unitOfWork.Villa.Update(Obj);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been updated successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }
        public IActionResult Delete(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa Obj)
        {
            Villa? objFromDb = _unitOfWork.Villa.Get(u => u.Id == Obj.Id); 
            if (objFromDb is not null)
            {
                if (!string.IsNullOrEmpty(objFromDb.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.Villa.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been deleted successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }
    }
}
