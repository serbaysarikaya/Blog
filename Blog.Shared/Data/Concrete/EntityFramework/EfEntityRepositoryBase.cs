﻿using Blog.Shared.Data.Abstract;
using Blog.Shared.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.Data.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;

        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<int> CountAsyc(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().CountAsync(predicate);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Remove(entity);});
        }

        public async Task<IList<TEntity>> GelAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includdeProperties)
        {
            IQueryable<TEntity> query= _context.Set<TEntity>();
            if (predicate!=null)
            {
               query= query.Where(predicate);
            }
            if (includdeProperties.Any())
            {
                foreach (var includdeProperty in includdeProperties)
                {
                    query = query.Include(includdeProperty);
                }
            }
            return await query.ToListAsync();

        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includdeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includdeProperties.Any())
            {
                foreach (var includdeProperty in includdeProperties)
                {
                    query = query.Include(includdeProperty);
                }
            }

            return await query.SingleOrDefaultAsync();

        }

        public async Task UpdateAsync(TEntity entity)
        {
             await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
        }
    }
}
