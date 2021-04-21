using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Services;

namespace VendasWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellersService;

        public SellersController(SellerService sellerService)
        {
            _sellersService = sellerService;
        }
        public IActionResult Index()
        {
            var list = _sellersService.FindAll();
            return View(list);
        }

    }
}
