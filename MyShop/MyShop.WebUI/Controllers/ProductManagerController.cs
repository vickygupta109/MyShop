using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        ProductRepository context;

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> lstProduct = context.Collection().ToList();
            return View(lstProduct);
        }

        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        public ActionResult Create() {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product p, string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else {
                if (!ModelState.IsValid)
                {
                    return View(p);
                }
                else
                {
                    product.Category = p.Category;
                    product.Description = p.Description;
                    product.Image = p.Image;
                    product.Name = p.Image;
                    product.Price = p.Price;

                    context.Commit();

                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}