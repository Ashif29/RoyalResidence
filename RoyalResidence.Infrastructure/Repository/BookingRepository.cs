using RoyalResidence.Application.Common.Interfaces;
using RoyalResidence.Domain.Entities;
using RoyalResidence.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalResidence.Infrastructure.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Booking entity)
        {
            _db.Bookings.Update(entity);
        }
    }
}
