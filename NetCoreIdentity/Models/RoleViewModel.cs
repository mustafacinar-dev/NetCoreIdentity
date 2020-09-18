using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentity.Models
{
    public class RoleViewModel
    {
        [Display(Name = "Yetki Adı")]
        [Required(ErrorMessage = "Yetki adı alanı gereklidir.")]
        public string Name { get; set; }
    }
}
