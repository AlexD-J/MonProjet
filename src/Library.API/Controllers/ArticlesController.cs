using System;
using System.Collections.Generic;
using AutoMapper;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/products/{productId}/articles")]
    public class ArticlesController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public ArticlesController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet()]
        public IActionResult GetArticlesForProduct(Guid productId)
        {
            if (!_libraryRepository.ProductExists(productId))
            {
                return NotFound();
            }

            var articlesFromRepo = _libraryRepository.GetArticlesForProduct(productId);
            var articles = Mapper.Map<IEnumerable<ArticleDto>>(articlesFromRepo);

            return Ok(articles);
        }
    }
}
