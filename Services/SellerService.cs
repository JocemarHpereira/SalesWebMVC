﻿using SalesWebMVC.Data;
using System.Collections.Generic;
using System.Linq;
using SalesWebMVC.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context) {
            _context = context;
        }

        public List<Seller> FindAll() {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj) {            
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id) {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id) {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
        
        public void Update(Seller obj) {
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not Found");
            }
            try
            {
                _context.SaveChanges();
                _context.Update(obj);
            }catch(DBConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }
    }
}
