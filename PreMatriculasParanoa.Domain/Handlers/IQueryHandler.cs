using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreMatriculasParanoa.Domain.Handlers;

public interface IByIdQueryHandler<out TViewModel>
    where TViewModel : IViewModel
{
    TViewModel Execute(int id);
}
public interface IByIdQueryHandlerAsync<TViewModel>
    where TViewModel : IViewModel
{
    Task<TViewModel> Execute(int id);
}

public interface IByFilterQueryHandler<out TViewModel, in TFilter>
    where TViewModel : IViewModel
    where TFilter : Filter
{
    TViewModel Execute(TFilter filtro);
}
public interface IByFilterQueryHandlerAsync<TViewModel, in TFilter>
    where TViewModel : IViewModel
    where TFilter : Filter
{
    Task<TViewModel> Execute(TFilter filtro);
}

public interface IDataTableQueryHandler<TViewModel, TFilter>
    where TViewModel : IViewModel
    where TFilter : Filter
{
    DataTableModel<TViewModel> Execute(TFilter filtro);
}
public interface IDataTableQueryHandlerAsync<TViewModel, TFilter>
    where TViewModel : IViewModel
    where TFilter : Filter
{
    Task<DataTableModel<TViewModel>> Execute(TFilter filtro);
}

public interface ISelectQueryHandler 
{
    IEnumerable<SelectResult> Execute(string search, int limit, int selected);
}
public interface ISelectQueryHandlerAsync
{
    Task<IEnumerable<SelectResult>> Execute(string search, int limit, int selected);
}

public interface ISelectQueryHandler<in TFilter>
    where TFilter : Filter
{
    IEnumerable<SelectResult> Execute(TFilter filtro);
}
public interface ISelectQueryHandlerAsync<in TFilter>
    where TFilter : Filter
{
    Task<IEnumerable<SelectResult>> Execute(TFilter filtro);
}