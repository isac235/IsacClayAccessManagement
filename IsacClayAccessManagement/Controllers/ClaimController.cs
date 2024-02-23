namespace IsacClayAccessManagement.Controllers
{
    using DTO;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService claimService;
        private readonly ILogger<ClaimController> logger;

        public ClaimController(IClaimService claimService, ILogger<ClaimController> logger)
        {
            this.claimService = claimService;
            this.logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateClaim([FromBody] ClaimDto claimDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await this.claimService.CreateClaimAsync(claimDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error creating claim");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
