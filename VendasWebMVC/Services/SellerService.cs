using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using VendasWebMVC.Services.Exceptions;

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

        //Atualização do Vendendor
        public void Update(Seller obj)
        {
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("ID não encontrado!");
            }
            //Caso tenha algum problema na atualização vai lançar um erro no catch.
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }            
        }
    }
}
