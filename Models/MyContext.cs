using Microsoft.EntityFrameworkCore;
using products_and_categories.Models;

namespace products_and_categories.Models
{
  public class MyContext : DbContext
  {
    // base() calls the parent class' constructor passing the "options" parameter along
    public MyContext(DbContextOptions options) : base(options) { }

    // "users" table is represented by this DbSet "Users"
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> ProductsCategories { get; set; }

  }

}