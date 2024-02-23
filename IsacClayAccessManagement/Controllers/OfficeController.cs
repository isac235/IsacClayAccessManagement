namespace IsacClayAccessManagement.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using DTO;
    using Infrastructure.CrossCutting.CustomExceptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Services.Interfaces;
    using Services.Mappers;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IOfficeService officeService;
        private readonly IUserService userService;
        private readonly IDoorService doorService;
        private readonly IScopeService scopeService;
        private readonly IUserRoleMappingService userRoleMappingService;
        private readonly IAccessEventService accessEventService;
        private readonly IUserClaimService userClaimService;
        private readonly ILogger<OfficeController> logger;

        public OfficeController(
            IConfiguration configuration,
            IOfficeService officeService,
            IUserService userService,
            IDoorService doorService,
            IScopeService scopeService,
            IUserRoleMappingService userRoleMappingService,
            IAccessEventService accessEventService,
            IUserClaimService userClaimService,
            ILogger<OfficeController> logger)
        {
            this.configuration = configuration;
            this.officeService = officeService;
            this.userService = userService;
            this.doorService = doorService;
            this.scopeService = scopeService;
            this.userRoleMappingService = userRoleMappingService;
            this.accessEventService = accessEventService;
            this.userClaimService = userClaimService;
            this.logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateOfficeAsync([FromBody] Office office)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the OfficeService to create the office
                var createdOffice = await this.officeService.CreateOfficeAsync(office).ConfigureAwait(false);

                return CreatedAtAction(nameof(CreateOfficeAsync), new { id = createdOffice.OfficeID }, createdOffice);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error creating office.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("{officeId}/user")]
        public async Task<IActionResult> CreateOfficeUserAsync(Guid officeId, [FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the DoorService to create the user
                var createdUser = await this.userService.CreateUserAsync(user, officeId).ConfigureAwait(false);

                return CreatedAtAction(nameof(CreateOfficeUserAsync), new { id = createdUser.UserId }, createdUser);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error creating office user.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("{officeId}/userRoleMapping")]
        public async Task<IActionResult> CreateOfficeUserRoleMappingAsync(Guid officeId, [FromBody] UserRoleMapping userRoleMapping)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the DoorService to create the userRoleMapping
                var createdUserRoleMapping = await this.userRoleMappingService.CreateUserRoleMappingAsync(userRoleMapping, officeId).ConfigureAwait(false);

                return CreatedAtAction(nameof(CreateOfficeUserRoleMappingAsync), new { id = createdUserRoleMapping.UserId }, createdUserRoleMapping);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error creating user role");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("{officeId}/door")]
        public async Task<IActionResult> CreateOfficeDoorAsync(Guid officeId, [FromBody] Door door)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the DoorService to create the door
                var createdDoor = await this.doorService.CreateDoorAsync(door, officeId).ConfigureAwait(false);

                return CreatedAtAction(nameof(CreateOfficeDoorAsync), new { id = createdDoor.DoorId }, createdDoor);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error creating office door");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("{officeId}/door/{doorId}/scope")]
        public async Task<IActionResult> CreateOfficeDoorScopeAsync(Guid officeId, Guid doorId, [FromBody] Scope scope)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the ScopeService to create the scope
                var createdScope = await this.scopeService.CreateScopeAsync(scope, doorId, officeId).ConfigureAwait(false);

                return CreatedAtAction(nameof(CreateOfficeDoorScopeAsync), new { id = createdScope.ScopeId }, createdScope);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error creating door scope");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("{officeId}/validateUser")]
        public async Task<IActionResult> ValidateOfficeUserAsync(Guid officeId, [FromBody] UserLoginRequest request)
        {
            try
            {
                // Call a service method to validate user credentials
                var user = await this.userService.ValidateUserCredentialsAsync(request, officeId).ConfigureAwait(false);


                if (user == null)
                {
                    return Unauthorized(); // User not authenticated
                }

                var userClaims = await this.userClaimService.GetUserClaimsByUserIdAsync(user.UserId).ConfigureAwait(false);

                var token = this.GenerateToken(userClaims);

                var result = new UserValidationResult
                {
                    UserValidation = user.MapUserToUserValidation(),
                    Token = token
                };

                // User authenticated, return user data
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error validating user");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("{officeId}/accessEvents")]
        public async Task<ActionResult<List<AccessEvent>>> GetAccessEventsByOfficeIdAsync(Guid officeId)
        {
            try
            {
                var result = await this.accessEventService.GetAccessEventsByOfficeIdAsync(officeId).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error gettting access events by officeId.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,Manager,User")]
        [HttpPost("{officeId}/accessEvent")]
        public async Task<IActionResult> AccessEventAsync(Guid officeId, [FromBody] AccessEvent accessEvent)
        {
            try
            {
                accessEvent.EventTime = DateTime.UtcNow;
                var result = await this.accessEventService.AccessEventAsync(accessEvent, officeId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error accessing event.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string GenerateToken(List<string> userClaims)
        {
            try
            {
                var claims = new List<Claim>();
                foreach (var claim in userClaims)
                {
                    claims.Add(new Claim(ClaimTypes.Role, claim));
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetSection("Authorization:SigningKey").Value));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(this.configuration.GetSection("Authorization:Issuer").Value,
                    this.configuration.GetSection("Authorization:Audience").Value,
                    claims.ToArray(),
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);

                var res = new JwtSecurityTokenHandler().WriteToken(token);
                return res;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Controller Error generating token");
                throw new ValidationException("Some error occurred while generation your validation token.", ex);
            }
        }
    }
}
