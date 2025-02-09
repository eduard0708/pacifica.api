using Microsoft.AspNetCore.Mvc;
using Pacifica.API.Dtos.Role;
using Pacifica.API.Services.RoleService;

namespace Pacifica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // Get all roles
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<RoleDto>>>> GetAllRoles()
        {
            try
            {
                var response = await _roleService.GetAllRolesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<List<string>> { Success = false, Message = $"Error retrieving roles: {ex.Message}" });
            }
        }

        // Create a new role
        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> CreateRole([FromBody] RoleDto roleName)
        {
            try
            {
                var response = await _roleService.CreateRoleAsync(roleName);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Message = $"Error creating role: {ex.Message}" });
            }
        }

        // // Assign a role to an employee
        // [HttpPost("assign/{employeeId}/{roleName}")]
        // public async Task<ActionResult<ApiResponse<bool>>> AssignRole(string employeeId, string roleName)
        // {
        //     try
        //     {
        //         var response = await _roleService.AssignRoleToEmployeeAsync(employeeId, roleName);
        //         return Ok(response);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new ApiResponse<bool> { Success = false, Message = $"Error assigning role: {ex.Message}" });
        //     }
        // }

        [HttpPost("assign/{Id}")]
        public async Task<ActionResult<ApiResponse<bool>>> AssignRoles(string Id, [FromBody] List<AssignRoleDto> roles)
        {
            try
            {
                if (roles == null || !roles.Any())
                {
                    return BadRequest(new ApiResponse<bool> { Success = false, Message = "No roles provided" });
                }

                // Call the service to handle assigning multiple roles
                var response = await _roleService.AssignRolesToEmployeeAsync(Id, roles);
                if (!response.Success)
                {
                    return BadRequest(new ApiResponse<bool> { Success = false, Message = response.Message });
                }

                return Ok(new ApiResponse<bool> { Success = true, Message = "Roles assigned successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<bool> { Success = false, Message = $"Error assigning roles: {ex.Message}" });
            }
        }


        // Remove a role from an employee
        [HttpPost("remove/{Id}/{roleName}")]
        public async Task<ActionResult<ApiResponse<bool>>> RemoveRole(string Id, string roleName)
        {
            try
            {
                var response = await _roleService.RemoveRoleFromEmployeeAsync(Id, roleName);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<bool> { Success = false, Message = $"Error removing role: {ex.Message}" });
            }
        }

        // Edit an existing role name
        [HttpPut("edit")]
        public async Task<ActionResult<ApiResponse<string>>> EditRole([FromBody] EditRoleRequest request)
        {
            try
            {
                // Call the service to edit the role
                var response = await _roleService.EditRoleAsync(request.OldRoleName!, request.NewRoleName!);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string> { Success = false, Message = $"Error updating role: {ex.Message}" });
            }
        }
    }

    // Helper class to bind the request body for editing a role
    public class EditRoleRequest
    {
        public string? OldRoleName { get; set; }
        public string? NewRoleName { get; set; }
    }
}
