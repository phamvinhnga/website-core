using Website.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Entity.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetListUserStaff();
    }
}
