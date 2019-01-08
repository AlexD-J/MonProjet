using System;
using System.Collections.Generic;
using AutoMapper;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/products")]
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

            var products = Mapper.Map<IEnumerable<ProductDto>>(productsFromRepo);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(Guid id)
        {
            var productFromRepo = _libraryRepository.GetProduct(id);

            if (productFromRepo == null)
            {
                return NotFound();
            }

            var product = Mapper.Map<ProductDto>(productFromRepo);

            return Ok(product);
        }
    }
}
