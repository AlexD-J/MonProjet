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
        //bool AuthorExists(Guid authorId);
        //IEnumerable<Product> GetBooksForAuthor(Guid authorId);
        //Product GetBookForAuthor(Guid authorId, Guid bookId);
        //void AddBookForAuthor(Guid authorId, Product product);
        //void UpdateBookForAuthor(Product product);
        //void DeleteBook(Product product);
        bool Save();
        IEnumerable<Product> GetProducts();
    }
}
