using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace products_and_categories.Models
{
  public class Category
  {
    [Key]
    public int CategoryId { get; set; }

    [Required (ErrorMessage = "Category name is required.")]
    [MinLength(2, ErrorMessage = "Category name name must be at least 2 characters long.")]
    [Display(Name = "Category Name")]
    public string CategoryName { get; set; }

    public List<ProductCategory> ProductsAssociated { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

  }
}