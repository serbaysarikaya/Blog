using Blog.Data.Abstract;
using Blog.Entities.Concrete;
using Blog.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Concrete.EntityFramework.Repositories
{
    public class ArticleRepository : EfEntityRepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(DbContext context) : base(context)
        {
        }
    }
}
