using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Data.Abstract;
using Blog.Entities.Concrete;
using Blog.Entities.Dtos;
using Blog.Services.Abstract;
using Blog.Shared.Utilities.Results.Abstract;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Blog.Shared.Utilities.Results.Concrete;


namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Succes, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GelAllAsync(null, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiç bir kategori bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDelete()
        {
            var categories = await _unitOfWork.Categories.GelAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "hiç Kategori bulunamadı", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleteAndActive()
        {
            var categories = await _unitOfWork.Categories.GelAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "hiç Kategori bulunamadı", null);
        }


        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createByName)
        {

            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createByName;
            category.ModifiedByName = createByName;
            await _unitOfWork.Categories.AddAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Succes, $"{categoryAddDto.Name} adlı Kategori başarıyla eklenmiştir.");
        }

        public async Task<IResult> Uptade(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedByName = modifiedByName;
            await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
            return new Result(ResultStatus.Succes, $"{categoryUpdateDto.Name} adlı Kategori başarıyla güncellenmistir.");
        }




        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(category => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Succes, $"{category.Name} adlı Kategori başarıyla silinmistir.");
            }
            return new Result(ResultStatus.Error, "Böler bir  kategori bulunamadı", null);


        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {


                await _unitOfWork.Categories.DeleteAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Succes, $"{category.Name} adlı Kategori Veritabanından silinmistir.");
            }
            return new Result(ResultStatus.Error, "Böler bir  kategori bulunamadı", null);
        }


    }
}
