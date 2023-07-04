using Website.Api.Filters;
using Website.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Shared.Dtos;
using Website.Api.Base.Controllers;
using Website.Shared.Entities;
using Website.Bal.Bases.Interfaces;

namespace Website.Api.Controllers
{
    [Route("api/specialized")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(AdminRoleFilter))]
    public class SpecializedController : CrudController<SpecializedController, Specialized, SpecializedInputDto, SpecializedOutputDto, SpecializedInputModel, SpecializedOutputModel, int>
    {
        public SpecializedController(
            IBaseManager<Specialized, SpecializedInputModel, SpecializedOutputModel, int> baseManager,
            ILogger<SpecializedController> logger
        ) : base (baseManager, logger)
        {
        }
    }
}
