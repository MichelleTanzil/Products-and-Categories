using Microsoft.EntityFrameworkCore;
using products_and_categories.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace products_and_categories.Controllers
{
  public class ProductCategoryController : Controller
  {
    private MyContext dbContext;
    // here we can "inject" our context service into the constructor
    public ProductCategoryController(MyContext context)
    {
      dbContext = context;
    }

    [HttpGet("products")]
    public IActionResult ProductView()
    {
      List<Product> AllProducts = dbContext.Products.ToList();
      return View(new ProductView {allproducts = AllProducts});
    }
    [HttpPost("products/new")]
    public IActionResult NewProduct(ProductView NewProduct)
    {
      if (ModelState.IsValid)
      {
        dbContext.Products.Add(NewProduct.product);
        dbContext.SaveChanges();
        return RedirectToAction("ProductView");
      }
      return View("ProductView");
    }

    [HttpGet("products/{productid}")]
    public IActionResult OneProductView(int productid)
    {
      Product retrivedProduct = dbContext.Products.FirstOrDefault(p => p.ProductId == productid);

      List<Category> IncludedCategories = dbContext.Categories
      .Include(p => p.ProductsAssociated)
      .ThenInclude(pc => pc.Product)
      .Where(p => p.ProductsAssociated.Any(pr => pr.ProductId == productid))
      .ToList();

      List<Category> ExcludedCategories = dbContext.Categories
      .Include(p => p.ProductsAssociated)
      .ThenInclude(pc => pc.Product)
      .Where(p => p.ProductsAssociated.All(pr => pr.ProductId != productid))
      .ToList();

      return View(new OneProductView{product=retrivedProduct, categoriesIncluded=IncludedCategories, categoriesExcluded = ExcludedCategories});
    }

    [HttpPost("products/{productid}")]
    public IActionResult NewCategoryForProduct(OneProductView NewCategoryForProduct)
    {
      int productid = NewCategoryForProduct.ProductCategory.Product.ProductId;
      Product retrivedProduct = dbContext.Products.FirstOrDefault(p => p.ProductId == productid);

      int categoryid = NewCategoryForProduct.ProductCategory.Category.CategoryId;
      Category retrivedCategory = dbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryid);

      ProductCategory NewCategoryProduct = new ProductCategory
      {
        ProductId = productid,
        CategoryId = categoryid,
        Product = retrivedProduct,
        Category = retrivedCategory,
      };
      dbContext.ProductsCategories.Add(NewCategoryProduct);
      dbContext.SaveChanges();
      return RedirectToAction("ProductView");
    }
    [HttpGet("categories")]
    public IActionResult CategoryView()
    {
      List<Category> AllCategories = dbContext.Categories.ToList();
      return View(new CategoryView { allcategories = AllCategories });
    }

    [HttpPost("categories/new")]
    public IActionResult NewCategory(CategoryView NewCategory)
    {
      if (ModelState.IsValid)
      {
        dbContext.Categories.Add(NewCategory.category);
        dbContext.SaveChanges();
        return RedirectToAction("CategoryView");
      }
      return View("CategoryView");
    }

    [HttpGet("categories/{categoryid}")]
    public IActionResult OneCategoryView(int categoryid)
    {
      Category retrivedCategory = dbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryid);

      List<Product> IncludedProducts = dbContext.Products
      .Include(c=> c.CategoriesAssociated)
      .ThenInclude(pc => pc.Product)
      .Where(p => p.CategoriesAssociated.Any(pr => pr.CategoryId == categoryid))
      .ToList();

      List<Product> ExcludedProducts = dbContext.Products
      .Include(p => p.CategoriesAssociated)
      .ThenInclude(pc => pc.Product)
      .Where(p => p.CategoriesAssociated.All(pr => pr.CategoryId != categoryid))
      .ToList();

      return View(new OneCategoryView { category = retrivedCategory, productsIncluded = IncludedProducts, productsExcluded = ExcludedProducts });
    }
    [HttpPost("categories/{categoryid}")]
    public IActionResult NewProductForCategory(OneCategoryView NewProductCategory)
    {
      int productid = NewProductCategory.ProductCategory.Product.ProductId;
      Product retrivedProduct = dbContext.Products.FirstOrDefault(p => p.ProductId == productid);

      int categoryid = NewProductCategory.ProductCategory.Category.CategoryId;
      Category retrivedCategory = dbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryid);

      ProductCategory NewCategoryProduct = new ProductCategory
      {
        ProductId = productid,
        CategoryId = categoryid,
        Product = retrivedProduct,
        Category = retrivedCategory,
      };
      dbContext.ProductsCategories.Add(NewCategoryProduct);
      dbContext.SaveChanges();
      return RedirectToAction("CategoryView")
    }
  }
}