using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CicekProjesiDAL;

namespace CicekProjesiBLL
{
    public class DetayManager
    {
        CicekProjesiEntities context = new CicekProjesiEntities();
        public Products GetProductById(int id)
        {
            try
            {
                
                return context.Products.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
        public void UpdateProduct(Products product)
        {
            //context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
