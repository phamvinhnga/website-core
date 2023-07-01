using Microsoft.EntityFrameworkCore;
using Website.Dal.Interfaces;
using Website.Shared.Bases.Models;
using Website.Dal;
using Website.Shared.Entities;
using Website.Shared.Bases.Repository;

namespace Website.Entity.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher, int>, ITeacherRepository
    {
        public TeacherRepository(DbContext context) : base(context) { }
    }
}
