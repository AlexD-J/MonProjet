using System;
using System.Collections.Generic;
using AutoMapper;
using Library.API.Entities;
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

        [HttpGet("{id}", Name= "GetArticleForProduct")]
        public IActionResult GetArticleForProduct(Guid productId, Guid id)
        {
            if (!_libraryRepository.ProductExists(productId))
            {
                return NotFound();
            }

            var articleFromRepo = _libraryRepository.GetArticleForProduct(productId, id);

            if (articleFromRepo == null)
            {
                return NotFound();
            }

            var article = Mapper.Map<ArticleDto>(articleFromRepo);

            return Ok(article);
        }

        [HttpPost()]
        public IActionResult CreateArticleForProduct(Guid productId, [FromBody] ArticleForCreationDto article)
        {
            if (article == null)
            {
                return BadRequest();
            }

            if (!_libraryRepository.ProductExists(productId))
            {
                return NotFound();
            }

            var articleEntity = Mapper.Map<Article>(article);

            _libraryRepository.AddArticleForProduct(productId, articleEntity);

            if (!_libraryRepository.Save())
            {
                throw new Exception("Creating an article failed on save.");
                //return StatusCode(500, "An unexpected fault happened. Try again later.");
            }

            var articleToReturn = Mapper.Map<ArticleDto>(articleEntity);

            return CreatedAtRoute("GetArticleForProduct", new { productId = productId, id = articleToReturn.Id }, articleToReturn);
        }
    }
}
