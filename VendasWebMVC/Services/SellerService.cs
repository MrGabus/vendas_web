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
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //Salva novos Venndedores no banco de dados
        public async Task InsetAsync(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        //Localiza um vendedor pelo Id
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        //Deleta o vendedor pelo Id
        public async Task RemoveAsync (int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        //Atualização do Vendendor
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("ID não encontrado!");
            }
            //Caso tenha algum problema na atualização vai lançar um erro no catch.
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }            
        }
    }
}
