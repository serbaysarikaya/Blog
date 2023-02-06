using Blog.Data.Abstract;
using Blog.Entities.Dtos;
using Blog.Services.Abstract;
using Blog.Shared.Utilities.Results.Abstract;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Blog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult> Add(ArticleAddDto articleAddDto, string createByName)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Delete(int articleId, string modifiedByName)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a=>a.Id ==articleId,a=>a.User,a=>a.Category);
            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Succes, new ArticleDto
                {
                    Article= article,
                    ResultStatus=ResultStatus.Succes
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, "Böylebir Makale Bulunmadı", null);


        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GelAllAsync(null, a => a.User, a => a.Category);
            if (articles.Count>-1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus=ResultStatus.Succes
                });

            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler Bulunamadı", null);


        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result) { 
            var articles = await _unitOfWork.Articles.GelAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.Category);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Succes
                    });

                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler Bulunamadı", null);

            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);


        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDelete()
        {
            var articles = await _unitOfWork.Articles.GelAllAsync(a => !a.IsDeleted, ar => ar.User, ar => ar.Category);
            if (articles.Count>-1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Succes
                });

            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler Bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeleteAndActive()
        {
            var articles = await _unitOfWork.Articles.GelAllAsync(a => !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Succes
                });

            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler Bulunamadı", null);

        }

        public Task<IResult> HardDelete(int articleId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Uptade(ArticleAddDto articleUpdateDto, string modifiedByName)
        {
            throw new NotImplementedException();
        }
    }
}
