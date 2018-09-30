using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public interface IComicsStoreRepository<T, TSearch>
        where TSearch : BasicSearchModel
    {
        Task<T> AddAsync(T value);
        Task DeleteAsync(T value);
        Task<List<T>> GetAsync(TSearch model);
        Task<T> GetAsync(int id);
        Task<T> UpdateAsync(T value);
    }
}