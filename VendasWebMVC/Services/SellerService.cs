using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace VendasWebMVC.Services
{
    public class SellerService
    {
        private readonly VendasWebMVCContext _context;

        public SellerService(VendasWebMVCContext context)
        {
            _context = context;
        }

        //Retorna todos Vendedores
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        //Salva novos Venndedores no banco de dados
        public void Inset(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        //Localiza um vendedor pelo Id
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        //Deleta o vendedor pelo Id
        public void Remove (int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}
