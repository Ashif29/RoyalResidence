using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoyalResidence.Application.Common.Interfaces;
using RoyalResidence.Application.Common.Utility;
using RoyalResidence.Domain.Entities;
using RoyalResidence.Infrastructure.Data;
using RoyalResidence.Web.ViewModels;
using System.Security.Claims;

namespace RoyalResidence.Web.Controllers
{

    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ApplicationUser user = _unitOfWork.User.Get(u => u.Id == userId);

            Booking booking = new()
            {
                VillaId = villaId,
                CheckInDate = checkInDate,
                CheckOutDate = checkInDate.AddDays(nights),
                Nights = nights,
                Villa = _unitOfWork.Villa.Get(u => u.Id == villaId, includeProperties: "VillaAmenity"),
                UserId = userId,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Name = user.Name
            };
            booking.TotalCost = booking.Villa.Price * nights;
            return View(booking);
        }

        [Authorize]
        [HttpPost]
        public IActionResult FinalizeBooking(Booking booking)
        {
            var villa = _unitOfWork.Villa.Get(u => u.Id ==  booking.VillaId);
            booking.TotalCost = villa.Price * booking.Nights;
            booking.BookingDate = DateTime.Now;
            booking.Status = SD.StatusPending;
            _unitOfWork.Booking.Add(booking);
            _unitOfWork.Save();
            return RedirectToAction("BookingConfirmation", "Booking", new {bookingId = booking.Id});
        }

        [Authorize]
        public IActionResult BookingConfirmation(int bookingId) 
        { 
            return View(bookingId);
        }
    }
}
