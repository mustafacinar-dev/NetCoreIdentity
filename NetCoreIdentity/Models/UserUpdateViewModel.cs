using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentity.Models
{
    public class UserUpdateViewModel
    {
        [Display(Name = "İsim:")]
        [Required(ErrorMessage = "Ad bilgisi boş geçilemez")]
        public string Name { get; set; }
        [Display(Name = "Soyisim:")]
        [Required(ErrorMessage = "Soyad bilgisi boş geçilemez")]
        public string SurName { get; set; }
        [Display(Name = "Resim:")]
        public string PictureUrl { get; set; }
        public IFormFile Picture { get; set; }
        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email bilgisi boş geçilemez")]
        [EmailAddress(ErrorMessage = "Geçerli bir mail adresi giriniz.")]
        public string Email { get; set; }
        [Display(Name = "Telefon:")]
        [Phone(ErrorMessage = "Geçerli bir numara giriniz.")]
        public string PhoneNumber { get; set; }
    }
}
