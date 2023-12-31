﻿using Website.Shared.Entities;
using Website.Shared.Bases.Interfaces;
using Website.Entity.Models;
using Website.Bal.Bases.Interfaces;

namespace Website.Bal.Interfaces
{
    public interface ISpecializedManager : IBaseManager<Specialized, SpecializedInputModel, SpecializedOutputModel, int>
    {
    }
}
