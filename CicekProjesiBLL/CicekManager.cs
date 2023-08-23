using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CicekProjesiDAL;

namespace CicekProjesiBLL
{
    public class CicekManager
    {
        CicekProjesiEntities context = new CicekProjesiEntities();
        public List<Products> GetAllCicek()
        {
            try
            {
                return context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<Products> GetFilteredProducts(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return context.Products.ToList();
            }
            else
            {
                return context.Products.Where(p => p.Name.Contains(search)).ToList();
            }
        }

        public bool AddProduct(Products newProduct)
        {
            try
            {
                if (newProduct != null)
                {
                    context.Products.Add(newProduct);
                    newProduct.ViewCount = 0;
                    context.SaveChanges();
                }
                return context.SaveChanges() > 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
