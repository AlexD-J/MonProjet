using System;
using System.Collections.Generic;
using AutoMapper;
using Library.API.Entities;
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

        [HttpGet("{id}", Name = "GetProduct")]
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

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductForCreationDto product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var productEntity = Mapper.Map<Product>(product);

            _libraryRepository.AddProduct(productEntity);

            if (!_libraryRepository.Save())
            {
                throw new Exception("Creating a product failed on save.");
                //return StatusCode(500, "An unexpected fault happened. Try again later.");
            }

            var productToReturn = Mapper.Map<ProductDto>(productEntity);

            return CreatedAtRoute("GetProduct", new { id = productToReturn.Id}, productToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var productFromRepo = _libraryRepository.GetProduct(id);
            if (productFromRepo == null)
            {
                return NotFound();
            }

            _libraryRepository.DeleteProduct(productFromRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception("Deleting an product failed on save");
            }

            return NoContent();
        }
    }


}
