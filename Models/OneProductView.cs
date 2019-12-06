using System.Collections.Generic;
namespace products_and_categories.Models
{
    public class OneProductView
    {
      public Product product { get; set; }
      public List<Category> categoriesIncluded { get; set; }
      public List<Category> categoriesExcluded { get; set; }
      public ProductCategory ProductCategory { get; set; }
    }
}