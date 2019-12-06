using System.Collections.Generic;
namespace products_and_categories.Models
{
    public class OneCategoryView
    {
    public Category category { get; set; }
    public List<Product> productsIncluded { get; set; }
    public List<Product> productsExcluded { get; set; }
    public ProductCategory ProductCategory { get; set; }

    }
}