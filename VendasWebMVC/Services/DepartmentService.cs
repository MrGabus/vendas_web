using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;

namespace VendasWebMVC.Services
{
    public class DepartmentService
    {
        private readonly VendasWebMVCContext _context;

        public DepartmentService(VendasWebMVCContext context)
        {
            _context = context;
        }

        public List<Department> FinbdAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

    }
}
