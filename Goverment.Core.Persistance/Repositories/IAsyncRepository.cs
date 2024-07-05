﻿using System.Linq.Expressions;
using Core.Persistence.Paging;
using Goverment.Core.Persistance.Repositories;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.Repositories;

public interface IAsyncRepository<T> : IQuery<T> where T : class, new()
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,
                      IIncludableQueryable<T, object>>? include = null,
                      bool enableTracking = true,
                      bool hasQueryFilterIgnore=false);

    Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                    int index = 0, int size = 10, bool enableTracking = true,
                                    CancellationToken cancellationToken = default,
                                    bool hasQueryFilterIgnore = false);
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool hasQueryFilterIgnore = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    /*    Task<IPaginate<T>> GetListByDynamicAsync(Dynamic.Dynamic dynamic,
                                                 Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                                 int index = 0, int size = 10, bool enableTracking = true,
                                                 CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool hasQueryFilterIgnore = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);
    */
    Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddAsync(IEnumerable<T> entity);
        Task<IEnumerable<T>> DeleteAsync(IEnumerable<T> entity);
        Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);

}