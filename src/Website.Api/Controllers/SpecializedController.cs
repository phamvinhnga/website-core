using Website.Api.Filters;
using Website.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Shared.Dtos;
using Website.Api.Base.Controllers;
using Website.Shared.Entities;
using Website.Bal.Bases.Interfaces;
using Website.Bal.Managers;

namespace Website.Api.Controllers
{
    [Route("api/specialized")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(AdminRoleFilter))]
    public class SpecializedController : CrudController<SpecializedController, SpecializedManager, Specialized, SpecializedInputDto, SpecializedOutputDto, SpecializedInputModel, SpecializedOutputModel, int>
    {
        public SpecializedController(SpecializedManager manager, ILogger<SpecializedController> logger) : base(manager, logger)
        {
        }
    }
}
