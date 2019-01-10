using Library.API.Entities;
using System;
using System.Collections.Generic;

namespace Library.API.Services
{
    public interface ILibraryRepository
    {
        bool Save();
        IEnumerable<Product> GetProducts();
        Product GetProduct(Guid productId);
        bool ProductExists(Guid productId);
        IEnumerable<Article> GetArticlesForProduct(Guid productId);
        Article GetArticleForProduct(Guid productId, Guid articleId);
        void AddProduct(Product productEntity);
        void AddArticleForProduct(Guid productId, Article articleEntity);
    }
}
