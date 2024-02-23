namespace IsacClayAccessManagement.Controllers
{
    using DTO;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;
    using System.Threading.Tasks;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;
        private readonly ILogger<RoleController> logger;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            this.roleService = roleService;
            this.logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] Role role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the RoleService to create the role
                var createdRole = await this.roleService.CreateRoleAsync(role).ConfigureAwait(false);

                return CreatedAtAction(nameof(CreateRoleAsync), new { id = createdRole.RoleId }, createdRole);
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, "Controller Error creating role.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
