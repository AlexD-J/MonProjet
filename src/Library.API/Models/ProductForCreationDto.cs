using System.Collections.Generic;

namespace Library.API.Models
{
    public class ProductForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public ICollection<ArticleForCreationDto> Articles { get; set; } = new List<ArticleForCreationDto>();
    }
}