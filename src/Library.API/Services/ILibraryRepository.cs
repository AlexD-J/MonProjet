using Library.API.Entities;
using System;
using System.Collections.Generic;

namespace Library.API.Services
{
    public interface ILibraryRepository
    {
        //UserAccount GetAuthor(Guid authorId);
        //IEnumerable<UserAccount> GetAuthors(IEnumerable<Guid> authorIds);
        //void AddAuthor(UserAccount userAccount);
        //void DeleteAuthor(UserAccount userAccount);
        //void UpdateAuthor(UserAccount userAccount);
        //IEnumerable<Product> GetBooksForAuthor(Guid authorId);
        //void AddBookForAuthor(Guid authorId, Product product);
        //void UpdateBookForAuthor(Product product);
        //void DeleteBook(Product product);
        bool Save();
        IEnumerable<Product> GetProducts();
        Product GetProduct(Guid productId);
        bool ProductExists(Guid productId);
        IEnumerable<Article> GetArticlesForProduct(Guid productId);
    }
}
