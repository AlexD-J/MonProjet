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

        //public void AddAuthor(UserAccount userAccount)
        //{
        //    userAccount.Id = Guid.NewGuid();
        //    _context.UserAccounts.Add(userAccount);

        //    // the repository fills the id (instead of using identity columns)
        //    if (userAccount.Products.Any())
        //    {
        //        foreach (var book in userAccount.Products)
        //        {
        //            book.Id = Guid.NewGuid();
        //        }
        //    }
        //}

        //public void AddBookForAuthor(Guid authorId, Product product)
        //{
        //    var author = GetAuthor(authorId);
        //    if (author != null)
        //    {
        //        // if there isn't an id filled out (ie: we're not upserting),
        //        // we should generate one
        //        if (product.Id == Guid.Empty)
        //        {
        //            product.Id = Guid.NewGuid();
        //        }
        //        author.Products.Add(product);
        //    }
        //}

        //public bool AuthorExists(Guid authorId)
        //{
        //    return _context.UserAccounts.Any(a => a.Id == authorId);
        //}

        //public void DeleteAuthor(UserAccount userAccount)
        //{
        //    _context.UserAccounts.Remove(userAccount);
        //}

        //public void DeleteBook(Product product)
        //{
        //    _context.Products.Remove(product);
        //}

        //public UserAccount GetAuthor(Guid authorId)
        //{
        //    return _context.UserAccounts.FirstOrDefault(a => a.Id == authorId);
        //}

        //public IEnumerable<UserAccount> GetAuthors(IEnumerable<Guid> authorIds)
        //{
        //    return _context.UserAccounts.Where(a => authorIds.Contains(a.Id))
        //        .OrderBy(a => a.FirstName)
        //        .OrderBy(a => a.LastName)
        //        .ToList();
        //}

        //public void UpdateAuthor(UserAccount userAccount)
        //{
        //    // no code in this implementation
        //}

        //public Product GetBookForAuthor(Guid authorId, Guid bookId)
        //{
        //    return _context.Products
        //      .Where(b => b.AuthorId == authorId && b.Id == bookId).FirstOrDefault();
        //}

        //public IEnumerable<Product> GetBooksForAuthor(Guid authorId)
        //{
        //    return _context.Products
        //                .Where(b => b.AuthorId == authorId).OrderBy(b => b.Title).ToList();
        //}

        //public void UpdateBookForAuthor(Product product)
        //{
        //    // no code in this implementation
        //}

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

        public IEnumerable<Article> GetArticlesForProduct(Guid productId)
        {
            return _context.Articles.Where(pa => pa.ProductId == productId)
                .OrderBy(pa => pa.Title).ToList().ToList();
        }
    }
}
