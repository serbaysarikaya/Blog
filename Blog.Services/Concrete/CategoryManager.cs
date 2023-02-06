using Blog.Data.Abstract;
using Blog.Entities.Concrete;
using Blog.Entities.Dtos;
using Blog.Services.Abstract;
using Blog.Shared.Utilities.Results.Abstract;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Blog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<Category>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<Category>(ResultStatus.Succes, category);
            }
            return new DataResult<Category>(ResultStatus.Error, "Böyle bir category bulunmadı.", null);
        }
        public async Task<IDataResult<IList<Category>>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GelAllAsync(null, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Succes, categories);
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "hiç Kategori bulunamadı", null);

        }

        public async Task<IDataResult<IList<Category>>> GetAllByNonDelete()
        {
            var categories = await _unitOfWork.Categories.GelAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Succes, categories);
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "hiç Kategori bulunamadı", null);
        }

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createByName)
        {
            await _unitOfWork.Categories.AddAsync(
                new Category
                {
                    Name = categoryAddDto.Name,
                    Description = categoryAddDto.Description,
                    Note = categoryAddDto.Note,
                    CreatedByName = createByName,
                    CreatedDate = DateTime.Now,
                    ModifiedByName = createByName,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false

                }).ContinueWith(t => _unitOfWork.SaveAsync());
            // await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Succes, $"{categoryAddDto.Name} adlı Kategori başarıyla eklenmiştir.");
        }

        public async Task<IResult> Uptade(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            if (category != null)
            {
                category.Name = categoryUpdateDto.Name;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive = categoryUpdateDto.IsActive;
                category.IsDeleted = categoryUpdateDto.IsDeleted;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Succes, $"{categoryUpdateDto.Name} adlı Kategori başarıyla güncellenmistir.");
            }
            return new Result(ResultStatus.Error, "Böler bir  kategori bulunamadı", null);

        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(category=> _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Succes, $"{category.Name} adlı Kategori başarıyla silinmistir.");
            }
            return new Result(ResultStatus.Error, "Böler bir  kategori bulunamadı", null);


        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
              
              
                await _unitOfWork.Categories.DeleteAsync(category).ContinueWith(t=>_unitOfWork.SaveAsync());
                return new Result(ResultStatus.Succes, $"{category.Name} adlı Kategori Veritabanından silinmistir.");
            }
            return new Result(ResultStatus.Error, "Böler bir  kategori bulunamadı", null);
        }

    }
}
