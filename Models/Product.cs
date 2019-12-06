using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace products_and_categories.Models
{
  public class Product
  {
    [Key]
    public int ProductId { get; set; }

    [Required (ErrorMessage = "Product name is required.")]
    [MinLength(2, ErrorMessage="Product name must be at least 2 characters long.")]
    [Display(Name = "Product Name")]
    public string ProductName { get; set; }

    [Required (ErrorMessage = "A description is required.")]
    [MinLength(2, ErrorMessage="Description name must be at least 2 characters long.")]
    public string Description { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    public List<ProductCategory> CategoriesAssociated { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

  }
}