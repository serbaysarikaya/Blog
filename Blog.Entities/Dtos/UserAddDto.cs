using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.Dtos
{
    public class UserAddDto
    {
        [DisplayName("Kullacı Adı")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} {1} de büyük olmamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} den kücük olmamalıdır")]
        public string UserName { get; set; }
        [DisplayName("Eposta Adresi")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} de büyük olmamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} den kücük olmamalıdır")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} de büyük olmamalıdır")]
        [MinLength(5, ErrorMessage = "{0} {1} den kücük olmamalıdır")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir.")]
        [MaxLength(13, ErrorMessage = "{0} {1} de büyük olmamalıdır")]
        [MinLength(13, ErrorMessage = "{0} {1} den kücük olmamalıdır")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DisplayName("Resim")]
        [Required(ErrorMessage = "Lütfen bir resim seçiniz")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
    }
}
