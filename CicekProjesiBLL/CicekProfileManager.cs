using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using CicekProjesiDAL;

namespace CicekProjesiBLL
{
    public class CicekProfileManager
    {
        CicekProjesiEntities context = new CicekProjesiEntities();
        public Products GetPersonelProile(int id)
        {
            try
            {
                return context.Products.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool CicekUpdate()
        {
            try
            {
                return context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
