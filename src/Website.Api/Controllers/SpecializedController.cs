using Website.Api.Filters;
using Website.Biz.Managers.Interfaces;
using Website.Entity.Model;
using Website.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.Extensions.Options;

namespace Website.Api.Controllers
{
    [Route("api/specialized")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(AdminRoleFilter))]
    public class SpecializedController : ControllerBase
    {
        private readonly ISpecializedManager _specializedManager;

        public SpecializedController(
            ISpecializedManager specializedManager
        ) 
        {
            _specializedManager = specializedManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _specializedManager.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] BasePageInputModel input)
        {
            return Ok(await _specializedManager.GetListAsync(input));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] SpecializedInputModel input)
        {
            await _specializedManager.CreateAsync(input, User.Claims.GetUserId());
            return Ok(true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] SpecializedInputModel input)
        {
            await _specializedManager.UpdateAsync(input, User.Claims.GetUserId());
            return Ok(true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _specializedManager.DeleteAsync(id);
            return Ok(true);
        }
    }
}
