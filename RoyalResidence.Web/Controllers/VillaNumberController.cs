using Microsoft.AspNetCore.Mvc;
using RoyalResidence.Domain.Entities;
using RoyalResidence.Infrastructure.Data;

namespace RoyalResidence.Web.Controllers
{
    public class VillaNumber : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaNumber(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
