using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoyalResidence.Application.Common.Interfaces;
using RoyalResidence.Application.Common.Utility;
using RoyalResidence.Domain.Entities;
using RoyalResidence.Infrastructure.Data;
using RoyalResidence.Web.ViewModels;

namespace RoyalResidence.Web.Controllers
{

    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
        {
            Booking booking = new()
            {
                VillaId = villaId,
                CheckInDate = checkInDate,
                CheckOutDate = checkInDate.AddDays(nights),
                Nights = nights,
                Villa = _unitOfWork.Villa.Get(u => u.Id == villaId, includeProperties: "VillaAmenity")
            };
            return View(booking);
        }
    }
}
