using Microsoft.EntityFrameworkCore;
using RoyalResidence.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalResidence.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) :base(options)
        {
            
        }
        public DbSet<Villa> Villas  { get; set; }
    }
}
