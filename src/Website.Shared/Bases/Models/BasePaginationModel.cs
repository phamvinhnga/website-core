using System.Collections.Generic;
using Website.Shared.Bases.Interfaces;

namespace Website.Shared.Bases.Models
{
    public partial class BasePaginationInputModel : IBasePaginationInput<BaseCriteriaRequestModel>
    {
        public int MaxCountResult { get; set; } = 999999;

        public int SkipCount { get; set; } = 0;

        public string Sorting { get; set; }

        public IList<BaseCriteriaRequestModel> ListCriterias { get; set; }
    }

    public partial class BasePaginationOutputModel<TEntity> : IBasePaginationOutput<TEntity> where TEntity : class
    {
        public IList<TEntity> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
