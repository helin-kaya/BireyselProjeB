using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CicekProjesiBLL;
using CicekProjesiDAL;
using CicekProjesiUI.Models;

namespace CicekProjesiUI.Controllers
{
    public class CicekController : Controller
    {
        CicekManager cicekManager = new CicekManager();
        [HttpGet]
        public ActionResult Index(string search)
        {
            try
            {
                var filteredProducts = cicekManager.GetFilteredProducts(search);
                return View(filteredProducts);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Beklenmedik bir hata oluştu!{ex.Message}");
                return View(new List<Products>());
            }
        }
        

        [HttpGet]
        public ActionResult Add()
        {
            return View(new CicekProfileViewModel());
        }

        [HttpPost]
        public ActionResult Add(CicekProfileViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                 var newFlower = new Products();
                 newFlower.Name = model.Name;
                 newFlower.Description = model.Description;
                 newFlower.Price = model.Price;
                newFlower.FlowerTypesId= model.FlowerTypesId;
                if (model.FlowerPictureUpload != null && model.FlowerPictureUpload.ContentType.Contains("" +
                   "image") && model.FlowerPictureUpload.ContentLength > 0)
                {
                    string fileName = $"{Guid.NewGuid().ToString().Replace("-", "")}";

                    string uzanti = Path.GetExtension(model.FlowerPictureUpload.FileName);
                    string directoryPath =
                        Server.MapPath($"~/FlowerPictures");
                    string filePath = Server.MapPath($"~/FlowerPictures/{fileName}{uzanti}");

                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    model.FlowerPictureUpload.SaveAs(filePath);
                    newFlower.FlowerPicture = $"/FlowerPictures/{fileName}{uzanti}";

                }
                else
                {
                    ModelState.AddModelError("", $"Lütfen doğru formatta veya yeterli boyutta resim seçiniz!");
                    return View(model);
                }
                cicekManager.AddProduct(newFlower);

                TempData["CicekAdd"] = "Çiçek başarıyla eklendi!";
                return RedirectToAction("Profile", "CicekProfile", new { id = newFlower.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Beklenmedik bir hata oluştu! {ex.Message}");
                return View(model);
            }
        }
    }
}