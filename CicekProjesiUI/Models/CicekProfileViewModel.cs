using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using CicekProjesiDAL;

namespace CicekProjesiUI.Models
{
    public class CicekProfileViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim boş geçilemez!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İsim alanı min 2 mak 50 karakter olmalıdır!")]
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Açıklama boş geçilemez!")]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Açıklama alanı min 2 mak 1000 karakter olmalıdır!")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? ViewCount { get; set; }
        public string FlowerPicture { get; set; }
        public HttpPostedFileBase FlowerPictureUpload { get; set; }
        public int FlowerTypesId { get; set; }

        public FlowerTypes FlowerTypes { get; set; }=new FlowerTypes();

    }
}