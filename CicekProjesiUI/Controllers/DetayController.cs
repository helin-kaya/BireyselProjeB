using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CicekProjesiBLL;
using CicekProjesiDAL;

namespace CicekProjesiUI.Controllers
{
    public class DetayController : Controller
    {
        DetayManager detayManager=new DetayManager();
        // GET: Detay
        public ActionResult Index(int id)
        {
            try
            {
                Products product = detayManager.GetProductById(id);
                if (product != null)
                {
                    product.ViewCount++;
                    detayManager.UpdateProduct(product);
                    return View(product);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Beklenmedik bir hata oluştu!{ex.Message}");
                return View(new List<Products>());
            }
        }
    }
}