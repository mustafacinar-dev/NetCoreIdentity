using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentity.Models
{
    public class UpdateRoleViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Yetki Adı")]
        [Required(ErrorMessage = "Yetki adı alanı gereklidir.")]
        public string Name { get; set; }
    }
}
