using System;

namespace Library.API.Models
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public Guid ProductId { get; set; }
    }
}
