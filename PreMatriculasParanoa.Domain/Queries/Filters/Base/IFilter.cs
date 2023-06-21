namespace PreMatriculasParanoa.Domain.Queries.Filters.Base
{
    public interface IFilter
    {
        string Search { get; set; }
        int? Page { get; set; }
        int? Limit { get; set; }
        string SortingField { get; set; }
        bool? Desc { get; set; }
    }
}