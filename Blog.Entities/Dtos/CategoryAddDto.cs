﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} {1} de büyük olmamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} den kücük olmamalıdır")]
        public string Name { get; set; }
        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} de büyük olmamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} den kücük olmamalıdır")]
        public string Description { get; set; }
        [DisplayName("Kategori Özel not alanı")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir.")]
        [MaxLength(500, ErrorMessage = "{0} {1} de büyük olmamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} den kücük olmamalıdır")]
        public string Note { get; set; }
        [DisplayName("Aktif mi?")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir.")]
           public bool IsActive { get; set; }
    }
}
