using Website.Api.Filters;
using Website.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Shared.Dtos;
using Website.Api.Base.Controllers;
using Website.Shared.Entities;
using Website.Bal.Bases.Interfaces;
using Website.Shared.Models;

namespace Website.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(AdminRoleFilter))]
    public class CategoryController : CrudController<CategoryController, Category, CategoryInputDto, CategoryOutputDto, CategoryInputModel, CategoryOutputModel, int>
    {
        public CategoryController(
            IBaseManager<Category, CategoryInputModel, CategoryOutputModel, int> baseManager,
            ILogger<CategoryController> logger
        ) : base (baseManager, logger)
        {
        }
    }
}
