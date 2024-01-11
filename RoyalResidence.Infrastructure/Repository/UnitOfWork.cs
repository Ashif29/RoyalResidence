﻿using RoyalResidence.Application.Common.Interfaces;
using RoyalResidence.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalResidence.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IVillaRepository Villa { get; private set; }
        public IVillaNumberRepository VillaNumber { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Villa = new VillaRepository(db);
            VillaNumber = new VillaNumberRepository(db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
