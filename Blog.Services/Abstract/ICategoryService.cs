using Blog.Entities.Concrete;
using Blog.Entities.Dtos;
using Blog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Blog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> GetAsync(int categoryId);
        /// <summary>
        /// Verilen ID parametresine ait kategori CategoryUpdateDto temsili geriye döner
        /// </summary>
        /// <param name="categoryId">0'dan büyü integer bir ID değeri</param>
        /// <returns>Asenkron bir operasyon ile Task olarak işlemn sonucunu DataResult tipinde geriye döner</returns>
        Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId);
        Task<IDataResult<CategoryListDto>> GetAllAsync();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync();
        /// <summary>
        /// Verilen CategoryAddDto ce CreatedByName parametrelerine ait bilgiler ile yeni bir category ekler.
        /// </summary>
        /// <param name="categoryAddDto"> categoryAddDto tipinde eklenecke kategori bilgileri</param>
        /// <param name="createdByName">string tipinde kullanıcının adı</param>
        /// <returns>Asenkeron bir operasyon ile Task olarak bize ekleme işleminin sonucu DAtaResult tipinde döner.</returns>
        Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName);
        Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int categoryId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
