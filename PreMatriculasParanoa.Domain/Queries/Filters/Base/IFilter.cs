namespace PreMatriculasParanoa.Domain.Queries.Filters.Base
{
    public interface IFilter
    {
        int Year { get; set; }
        string Search { get; set; }
        int? Page { get; set; }
        int? Limit { get; set; }
        string SortingField { get; set; }
        bool? Desc { get; set; }
    }
}