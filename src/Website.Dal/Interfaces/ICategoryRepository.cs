﻿using Website.Dal.Bases.Interfaces;
using Website.Shared.Entities;

namespace Website.Dal.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category, int>
    {
    }
}
