using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Entities;
using Library.API.Helpers;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/productcollections")]
    public class ProductCollectionsController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public ProductCollectionsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpPost]
        public IActionResult CreateProductCollection([FromBody] IEnumerable<ProductForCreationDto> productCollection)
        {
            if (productCollection == null)
            {
                return BadRequest();
            }

            var productEntities = Mapper.Map<IEnumerable<Product>>(productCollection);

            foreach (var product in productEntities)
            {
                _libraryRepository.AddProduct(product);
            }

            if (!_libraryRepository.Save())
            {
                throw new Exception("An error occur while saving a collection of product");
            }

            var productCollectionToReturn = Mapper.Map<IEnumerable<ProductDto>>(productEntities);
            var idsAsString = string.Join(",", productCollectionToReturn.Select(p => p.Id));

            return CreatedAtRoute("GetProductCollection", new {ids = idsAsString}, productCollectionToReturn);
        }

        [HttpGet("({ids})", Name = "GetProductCollection")]
        public IActionResult GetProductCollections([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var productEntities = _libraryRepository.GetProducts(ids);

            if (ids.Count() != productEntities.Count())
            {
                return NotFound();
            }

            var productsToReturn = Mapper.Map<IEnumerable<ProductDto>>(productEntities);

            return Ok(productsToReturn);
        }
    }
}