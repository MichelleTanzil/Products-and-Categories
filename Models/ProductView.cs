using System.Collections.Generic;

namespace products_and_categories.Models
{
    public class ProductView
    {
      public Product product { get; set; }
      public List<Product> allproducts { get; set; }
  }
}