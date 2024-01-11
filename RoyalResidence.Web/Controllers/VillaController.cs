using Microsoft.AspNetCore.Mvc;
using RoyalResidence.Application.Common.Interfaces;
using RoyalResidence.Domain.Entities;
using RoyalResidence.Infrastructure.Data;

namespace RoyalResidence.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                _unitOfWork.Villa.Add(Obj);
                _unitOfWork.Villa.Save();
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
                _unitOfWork.Villa.Update(Obj);
                _unitOfWork.Villa.Save();
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
                _unitOfWork.Villa.Remove(objFromDb);
                _unitOfWork.Villa.Save();
                TempData["success"] = "The villa has been deleted successfully.";
                return RedirectToAction("Index", "Villa");
            }
            return View();
        }
    }
}
