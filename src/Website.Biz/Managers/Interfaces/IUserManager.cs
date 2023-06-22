using Website.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Biz.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<List<StaffOutputModel>> GetListStaffAsync();
        Task RegisterStaffAsync(StaffRegisterInputModel input);
    }
}
