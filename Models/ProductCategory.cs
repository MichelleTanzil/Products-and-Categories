using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace products_and_categories.Models
{
    public class ProductCategory
    {
    [Key]
    public int ProductCategoryId { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public Product Product { get; set; }
    public Category Category { get; set; }
    }
}