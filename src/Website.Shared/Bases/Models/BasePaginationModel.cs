using System.Collections.Generic;
using Website.Shared.Bases.Interfaces;

namespace Website.Shared.Bases.Models
{
    public partial class BasePaginationInputModel : IBasePaginationInput<BaseCriteriaRequestModel>
    {
        public virtual int MaxCountResult { get; set; } = 999999;

        public virtual int SkipCount { get; set; } = 0;

        public virtual string Sorting { get; set; }

        public virtual IList<BaseCriteriaRequestModel> ListCriterias { get; set; }
    }

    public partial class BasePaginationOutputModel<TEntity> : IBasePaginationOutput<TEntity> where TEntity : class
    {
        public virtual IList<TEntity> Items { get; set; }
        public virtual int TotalCount { get; set; }
    }
}
