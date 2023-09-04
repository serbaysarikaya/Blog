using Blog.Entities.Concrete;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Mvc.Areas.Admin.Models
{
    public class ArticleAddViewModel
    {


        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalı")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalı")]
        public string Title { get; set; }

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MinLength(20, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalı")]
        public string Content { get; set; }

        [DisplayName("Küçük Resim")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public IFormFile ThumnailFile { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Yazar")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalı")]
        [MinLength(1, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalı")]
        public string SeoAuthor { get; set; }

        [DisplayName("Makale Açıklmamsı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(150, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalı")]
        [MinLength(1, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalı")]
        public string SeoDescription { get; set; }

        [DisplayName("Makale Etiketleri")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalı")]
        [MinLength(1, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalı")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public int CategoryId { get; set; }


        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public bool IsActive { get; set; }

        public IList<Category> Categories { get; set; }
    }
}
