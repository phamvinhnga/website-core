using Microsoft.EntityFrameworkCore;
using Website.Dal.Interfaces;
using Website.Dal;
using Website.Shared.Entities;
using Website.Shared.Bases.Models;

namespace Website.Entity.Repositories
{
    public class ParentRepository : IParentRepository
    {
        private readonly ApplicationDbContext _context;

        public ParentRepository(
            ApplicationDbContext context
        )
        {
            _context = context;
        }
    }
}
