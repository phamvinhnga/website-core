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
    [Route("api/post")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostManager _postManager;

        public PostController(
            IPostManager postManager
        ) 
        {
            _postManager = postManager;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _postManager.GetByIdAsync(id));
        }

        [HttpGet]
        [ServiceFilter(typeof(AdminRoleFilter))]
        public async Task<IActionResult> GetListAsync([FromQuery] BasePageInputModel input)
        {
            return Ok(await _postManager.GetListAsync(input));
        }

        [HttpPost]
        [ServiceFilter(typeof(AdminRoleFilter))]
        public async Task<IActionResult> CreateAsync([FromBody] PostInputModel input)
        {
            await _postManager.CreateAsync(input, User.Claims.GetUserId());
            return Ok(true);
        }

        [HttpPut]
        [ServiceFilter(typeof(AdminRoleFilter))]
        public async Task<IActionResult> UpdateAsync([FromBody] PostInputModel input)
        {
            await _postManager.UpdateAsync(input, User.Claims.GetUserId());
            return Ok(true);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(AdminRoleFilter))]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PostInputModel input)
        {
            await _postManager.UpdateAsync(input, User.Claims.GetUserId());
            return Ok(true);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AdminRoleFilter))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _postManager.DeleteAsync(id);
            return Ok(true);
        }
    }
}
