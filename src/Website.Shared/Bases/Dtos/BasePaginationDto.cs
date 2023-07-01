using System.Collections.Generic;
using Website.Shared.Bases.Interfaces;

namespace Website.Shared.Bases.Dtos
{
    public partial class BasePaginationInputDto : IBasePaginationInput<BaseCriteriaRequestDto>
    {
        public int MaxCountResult { get; set; } = 999999;

        public int SkipCount { get; set; } = 0;

        public string Sorting { get; set; }

        public IList<BaseCriteriaRequestDto> ListCriterias { get; set; }
    }

    public partial class BasePaginationOutputDto<TEntity> : IBasePaginationOutput<TEntity> where TEntity : class
    {
        public IList<TEntity> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
