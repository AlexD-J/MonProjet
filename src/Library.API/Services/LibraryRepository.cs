using Library.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Services
{
    public class LibraryRepository : ILibraryRepository
    {
        private LibraryContext _context;

        public LibraryRepository(LibraryContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.OrderBy(a => a.Name);
        }

        public Product GetProduct(Guid productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }

        public bool ProductExists(Guid productId)
        {
            return _context.Products.Any(p => p.Id == productId);
        }

        public void AddProduct(Product productEntity)
        {
            productEntity.Id = Guid.NewGuid();
            _context.Products.Add(productEntity);

            if (productEntity.Articles.Any())
            {
                foreach (var article in productEntity.Articles)
                {
                    article.Id = Guid.NewGuid();
                }
            }
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public IEnumerable<Article> GetArticlesForProduct(Guid productId)
        {
            return _context.Articles.Where(pa => pa.ProductId == productId)
                .OrderBy(pa => pa.Title).ToList().ToList();
        }

        public Article GetArticleForProduct(Guid productId, Guid articleId)
        {
            return _context.Articles.FirstOrDefault(pa => pa.ProductId == productId && pa.Id == articleId);
        }

        public void AddArticleForProduct(Guid productId, Article articleEntity)
        {
            var product = GetProduct(productId);
            if (product != null)
            {
                // if there isn't an id filled out (ie: we're not upserting),
                // we should generate one
                if (articleEntity.Id == Guid.Empty)
                {
                    articleEntity.Id = Guid.NewGuid();
                }
                product.Articles.Add(articleEntity);
            }
        }

        public void DeleteArticle(Article article)
        {
            _context.Articles.Remove(article);
        }
    }
}
