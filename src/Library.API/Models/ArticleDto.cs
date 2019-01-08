using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ProductId { get; set; }
    }
}
