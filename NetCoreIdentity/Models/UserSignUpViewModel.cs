using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name="Kullanıcı Adı:")]
        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez")]
        public string UserName { get; set; }
        [Display(Name = "Şifre:")]
        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }
        [Display(Name = "Şifre Tekrar:")]
        [Compare("Password",ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "İsim:")]
        [Required(ErrorMessage = "Ad bilgisi boş geçilemez")]
        public string Name { get; set; }
        [Display(Name = "Soyisim:")]
        [Required(ErrorMessage = "Soyad bilgisi boş geçilemez")]
        public string Surname { get; set; }
        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email bilgisi boş geçilemez")]
        public string Email { get; set; }
        [Display(Name = "Cinsiyet:")]
        [Required(ErrorMessage = "Cinsiyet bilgisi boş geçilemez")]
        public string Gender { get; set; }
    }
}
