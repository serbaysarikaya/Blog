﻿using Blog.Entities.Concrete;
using Blog.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Abstract
{
    public interface ICategoryRepository:IEntityRepository<Category>
    {
        Task<Category> GetById(int categoryId);
    }
}
