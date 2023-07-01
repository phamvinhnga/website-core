using System;
using System.Collections.Generic;

namespace Website.Shared.Bases.Interfaces
{
    public interface IBasePaginationInput<TCriteriaRequest> where TCriteriaRequest : IBaseCriteriaRequest
    {
        public int MaxCountResult { get; set; }

        public int SkipCount { get; set; }

        public string Sorting { get; set; }

        public IList<TCriteriaRequest> ListCriterias { get; set; }
    }

    public interface IBasePaginationOutput<TEntity> where TEntity : class
    {
        public IList<TEntity> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
