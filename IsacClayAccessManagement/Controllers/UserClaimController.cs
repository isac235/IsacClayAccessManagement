namespace IsacClayAccessManagement.Controllers
{
    using System;
    using System.Threading.Tasks;
    using DTO;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserClaimController : ControllerBase
    {
        private readonly IUserClaimService userClaimService;

        public UserClaimController(IUserClaimService userClaimService)
        {
            this.userClaimService = userClaimService;
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
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
