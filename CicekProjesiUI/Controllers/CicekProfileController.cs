using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CicekProjesiBLL;
using CicekProjesiUI.Models;

namespace CicekProjesiUI.Controllers
{
    
    public class CicekProfileController : Controller
    {
        CicekProfileManager cicekProfileManager = new CicekProfileManager();
        [HttpGet]
        public ActionResult Profile(int id)
        {
            var product =cicekProfileManager.GetPersonelProile(id);
            CicekProfileViewModel model = new CicekProfileViewModel()
            {
                Id=product.Id,
                Name=product.Name,
                Description=product.Description,
                Price=product.Price,
                ViewCount=product.ViewCount,
                FlowerPicture=product.FlowerPicture,

            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Profile(CicekProfileViewModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);

                }
                var guncellenecekPersonel = cicekProfileManager.GetPersonelProile(model.Id);
                if (guncellenecekPersonel == null)
                {
                    ModelState.AddModelError("", $"Çiçek bulunmadı!");
                    return View(model);

                }
                guncellenecekPersonel.Name = model.Name;
                guncellenecekPersonel.Description = model.Description;
                guncellenecekPersonel.Price = model.Price;

                #region ProfilResmi
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
                    guncellenecekPersonel.FlowerPicture = $"/FlowerPictures/{fileName}{uzanti}";

                }
                else
                {
                    ModelState.AddModelError("", $"Lütfen doğru formatta veya yeterli boyutta resim seçiniz!");
                    return View(model);
                }


                #endregion 

                if (cicekProfileManager.CicekUpdate())
                {
                    TempData["CicekUpdate"] = "Cicek bilgileri güncellendi!";
                    return RedirectToAction("Profile", "CicekProfile", new { id = model.Id });
                }
                else
                {
                    ModelState.AddModelError("", $"Bilgilerinizi güncellerken hata oluştu!");
                    return View(model);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Beklenmedik bir hata oluştu! {ex.Message}");
                return View(model);
            }
        }
    }
}