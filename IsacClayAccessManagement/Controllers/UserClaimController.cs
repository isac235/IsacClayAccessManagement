namespace IsacClayAccessManagement.Controllers
{
    using System;
    using System.Threading.Tasks;
    using DTO;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserClaimController : ControllerBase
    {
        private readonly IUserClaimService userClaimService;
        private readonly ILogger<UserClaimController> logger;

        public UserClaimController(IUserClaimService userClaimService, ILogger<UserClaimController> logger)
        {
            this.userClaimService = userClaimService;
            this.logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AssociateUserWithClaim([FromBody] UserClaim userClaimDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await this.userClaimService.AssociateUserWithClaimAsync(userClaimDto.UserID, userClaimDto.ClaimID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error associating user with claim");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
