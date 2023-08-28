using AutoMapper;
using Blog.Data.Abstract;
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
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<int>> Count()
        {
            var commentCount = await _unitOfWork.Comments.CountAsyc();

            return commentCount > -1 ? new DataResult<int>(ResultStatus.Success, commentCount) : new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılasıldı.", -1);

        }

        public async Task<IDataResult<int>> CountIsDeleted()
        {

            var commentCount = await _unitOfWork.Comments.CountAsyc(co=>!co.IsDeleted);

            return commentCount > -1 ? new DataResult<int>(ResultStatus.Success, commentCount) : new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılasıldı.", -1);
        }
    }
}
