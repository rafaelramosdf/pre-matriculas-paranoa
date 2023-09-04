using System;

namespace PreMatriculasParanoa.Domain.Queries.Filters.Base
{
    public class Filter : IFilter
    {
        public string Search { get; set; } = "";
        public int? Page { get; set; } = 1;
        public int? Limit { get; set; } = 10;
        public string SortingField { get; set; } = "";
        public bool? Desc { get; set; } = false;
        public int Year { get; set; } = (DateTime.Now.Year + 1);
    }
}