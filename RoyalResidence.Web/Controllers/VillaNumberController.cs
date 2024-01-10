using Microsoft.AspNetCore.Mvc;
using RoyalResidence.Domain.Entities;
using RoyalResidence.Infrastructure.Data;

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
            var villaNumbers = _db.VillaNumbers.ToList();
            return View(villaNumbers);
        }
    }
}
