using AutoMapper;
using PreMatriculasParanoa.Domain.Queries.Base;
using PreMatriculasParanoa.Domain.Queries.Filters.Base;
using System.Collections.Generic;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Models.Base
{
    public class DataTableModel<TModel>
    {
        public IEnumerable<TModel> Data { get; set; } = new List<TModel>();
        public DataTableFilterModel Filter { get; set; } = new DataTableFilterModel();
        public DataTableSortingModel Sorting { get; set; } = new DataTableSortingModel();
        public DataTablePaginationModel Pagination { get; set; } = new DataTablePaginationModel();

        public void SortAndPage<TEntity, TFilter, TQuery> (IQueryable<TEntity> queryLinq, TFilter filter, TQuery query, IMapper mapper)
            where TEntity : Entity
            where TFilter : Filter
            where TQuery : IBaseQuery<TEntity, TFilter>
        {
            Filter = new DataTableFilterModel 
            {
                Search = filter.Search
            };

            Sorting = new DataTableSortingModel 
            { 
                Field = filter.SortingField, 
                Desc = filter.Desc ?? false 
            };

            Pagination = new DataTablePaginationModel 
            { 
                Limit = filter.Limit ?? 10, 
                Page = filter.Page ?? 1, 
                Total = queryLinq.Count() 
            };

            var sortingExpression = query.ObterOrdenacao(Sorting.Field);

            queryLinq = Sorting.Desc 
                ? queryLinq.OrderByDescending(sortingExpression) 
                : queryLinq.OrderBy(sortingExpression);

            queryLinq = queryLinq.Skip((Pagination.Page - 1) * Pagination.Limit).Take(Pagination.Limit);

            Data = mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(queryLinq.ToList());
        }
    }

    public class DataTableSortingModel
    {
        public string Field { get; set; }
        public bool Desc { get; set; } = false;
    }

    public class DataTablePaginationModel
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public int Total { get; set; }
    }

    public class DataTableFilterModel
    {
        public string Search { get; set; }
    }
}