﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {

        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product p)
        {
            Product productToUpdate = products.Find(m => m.Id == p.Id);

            if (productToUpdate != null)
            {
                productToUpdate = p;
            }
            else {
                throw new Exception("Product not found");
            }
        }

        public Product Find(string Id) {
            Product product = products.Find(m => m.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(m => m.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else {
                throw new Exception("No Product Found To Delete");
            }
        }
    }
}
