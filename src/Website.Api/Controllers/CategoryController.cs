using Website.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Shared.Dtos;
using Website.Api.Base.Controllers;
using Website.Shared.Entities;
using Website.Shared.Models;
using Website.Bal.Managers;

namespace Website.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(AdminRoleFilter))]
    public class CategoryController : CrudController<CategoryController, CategoryManager, Category, CategoryInputDto, CategoryOutputDto, CategoryInputModel, CategoryOutputModel, int>
    {
        public CategoryController(CategoryManager manager, ILogger<CategoryController> logger) : base(manager, logger)
        {
        }
    }
}
