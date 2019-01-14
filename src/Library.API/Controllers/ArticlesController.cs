using System;
using System.Collections.Generic;
using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpDelete("{id}")]
        public IActionResult DeleteArticleForProduct(Guid productId, Guid id)
        {
            if (!_libraryRepository.ProductExists(productId))
            {
                return NotFound();
            }

            var articleForProductRepo = _libraryRepository.GetArticleForProduct(productId, id);
            if (articleForProductRepo == null)
            {
                return NotFound();
            }

            _libraryRepository.DeleteArticle(articleForProductRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception("Deleting an article failed on save");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateArticleForProduct(Guid productId, Guid id, 
            [FromBody] ArticleForUpdateDto article)
        {
            if (article == null)
            {
                return BadRequest();
            }

            if (!_libraryRepository.ProductExists(productId))
            {
                return NotFound();
            }

            var articleForProductRepo = _libraryRepository.GetArticleForProduct(productId, id);
            if (articleForProductRepo == null)
            {
                var articleToAdd = Mapper.Map<Article>(article);
                articleToAdd.Id = id;
                _libraryRepository.AddArticleForProduct(productId, articleToAdd);

                if (!_libraryRepository.Save())
                {
                    throw new Exception("Upserting failed on save");
                }

                var articleToReturn = Mapper.Map<ArticleDto>(articleToAdd);

                return CreatedAtRoute("GetArticleForProduct", 
                    new {productId = productId, id = articleToReturn.Id}, 
                    articleToReturn);

            }

            Mapper.Map(article, articleForProductRepo);

            _libraryRepository.UpdateArticleForProduct(articleForProductRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception("$Updating failed on save");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchArticleForProduct(Guid productId, Guid id,
            [FromBody] JsonPatchDocument<ArticleForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_libraryRepository.ProductExists(productId))
            {
                return NotFound();
            }

            var articleForProductFromRepo = _libraryRepository.GetArticleForProduct(productId, id);
            if (articleForProductFromRepo == null)
            {
                return NotFound();
            }

            var articleToPatch = Mapper.Map<ArticleForUpdateDto>(articleForProductFromRepo);
            patchDoc.ApplyTo(articleToPatch);

            //validation ici

            Mapper.Map(articleToPatch, articleForProductFromRepo);

            _libraryRepository.UpdateArticleForProduct(articleForProductFromRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception("Patch failed on save");
            }

            return NoContent();
        }
    }
}
