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

        /// <summary>
        /// Executa a query de ordenacao e paginacao, consolida a consulta no banco de dados, e converte o resultado para o objeto "Data".
        /// </summary>
        /// <typeparam name="TEntity">Entidade do banco de dados</typeparam>
        /// <typeparam name="TFilter">Modelo de filtro com os campos para consulta</typeparam>
        /// <typeparam name="TQuery">Modelo com as expressoes (queries) para busca</typeparam>
        public void ExecuteQuerySortAndPage<TEntity, TFilter, TQuery>(IQueryable<TEntity> queryLinq, TFilter filter, TQuery query, IMapper mapper)
            where TEntity : Entity
            where TFilter : Filter
            where TQuery : IBaseQuery<TEntity, TFilter>
        {
            queryLinq = GetQuerySortAndPage(queryLinq, filter, query);
            Data = mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(queryLinq.ToList());
        }

        /// <summary>
        /// Executa a query passada via parametros, consolida a consulta no banco de dados, e converte o resultado para o objeto "Data".
        /// </summary>
        public void ExecuteQuery<TEntity>(IQueryable<TEntity> queryLinq, IMapper mapper)
            where TEntity : Entity
        {
            Data = mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(queryLinq.ToList());
        }

        /// <summary>
        /// Obtem somente a query com a ordenacao e paginacao dos registros.
        /// </summary>
        public IQueryable<TEntity> GetQuerySortAndPage<TEntity, TFilter, TQuery>(IQueryable<TEntity> queryLinq, TFilter filter, TQuery query)
            where TEntity : Entity
            where TFilter : Filter
            where TQuery : IBaseQuery<TEntity, TFilter>
        {
            queryLinq = GetQuerySort(queryLinq, filter, query);
            queryLinq = GetQueryPage(queryLinq, filter);
            return queryLinq;
        }

        /// <summary>
        /// Obtem somente a query de ordenacao dos registros.
        /// </summary>
        public IQueryable<TEntity> GetQuerySort<TEntity, TFilter, TQuery>(IQueryable<TEntity> queryLinq, TFilter filter, TQuery query)
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

            var sortingExpression = query.ObterOrdenacao(Sorting.Field);

            queryLinq = Sorting.Desc
                ? queryLinq.OrderByDescending(sortingExpression)
                : queryLinq.OrderBy(sortingExpression);

            return queryLinq;
        }

        /// <summary>
        /// Obtem somente a query de paginacao dos registros.
        /// </summary>
        public IQueryable<TEntity> GetQueryPage<TEntity, TFilter>(IQueryable<TEntity> queryLinq, TFilter filter)
            where TEntity : Entity
            where TFilter : Filter
        {
            Pagination = new DataTablePaginationModel
            {
                Limit = filter.Limit ?? 10,
                Page = filter.Page ?? 1,
                Total = queryLinq.Count()
            };

            queryLinq = queryLinq.Skip((Pagination.Page - 1) * Pagination.Limit).Take(Pagination.Limit);
            return queryLinq;
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