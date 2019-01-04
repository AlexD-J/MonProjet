using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public ProductsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public IActionResult GetProducts()
        {
            var productsFromRepo = _libraryRepository.GetProducts();
            return new JsonResult(productsFromRepo);
        }
    }
}
