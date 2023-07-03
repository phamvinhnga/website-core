using System.ComponentModel.DataAnnotations;
using Website.Api.Filters;
using Website.Entity.Model;
using Website.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Bal.Interfaces;
using Website.Shared.Bases.Models;

namespace Website.Api.Controllers
{
    [Route("api/parent")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(AdminRoleFilter))]
    public class ParentController : ControllerBase
    {
        private readonly IParentManager _parentManager;

        public ParentController(
            IParentManager parentManager
        ) 
        {
            _parentManager = parentManager;
        }

        //[HttpPut("index-page/{id:int}")]
        //public async Task<IActionResult> SetIsDisplayIndexPageAsync([Required] int id, [Required] bool isDisplayIndexPage)
        //{
        //    return Ok(await _parentManager.SetIsDisplayIndexPageAsync(id, isDisplayIndexPage));
        //}
    }
}
