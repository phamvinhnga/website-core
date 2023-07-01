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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _parentManager.GetByIdAsync(id));
        }

        [HttpPut("index-page/{id:int}")]
        public async Task<IActionResult> SetIsDisplayIndexPageAsync([Required] int id, [Required] bool isDisplayIndexPage)
        {
            return Ok(await _parentManager.SetIsDisplayIndexPageAsync(id, isDisplayIndexPage));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] BasePageInputModel input)
        {
            return Ok(await _parentManager.GetListAsync(input));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ParentInputModel input)
        {
            await _parentManager.CreateAsync(input, User.Claims.GetUserId());
            return Ok(true);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ParentInputModel input)
        {
            await _parentManager.UpdateAsync(input, User.Claims.GetUserId());
            return Ok(true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ParentInputModel input)
        {
            await _parentManager.UpdateAsync(input, User.Claims.GetUserId());
            return Ok(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _parentManager.DeleteAsync(id);
            return Ok(_parentManager.DeleteAsync(id));
        }
    }
}
