using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Library.API.Entities
{
    public class ProductArticle
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public Guid ProductId { get; set; }

    }
}