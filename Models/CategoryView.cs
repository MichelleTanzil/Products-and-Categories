using System.Collections.Generic;

namespace products_and_categories.Models
{
    public class CategoryView
    {
    public Category category { get; set; }
    public List<Category> allcategories { get; set; }
    }
}