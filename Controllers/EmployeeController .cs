using Microsoft.AspNetCore.Mvc;
using Pacifica.API.Dtos.Admin;
using Pacifica.API.Dtos.Employee;
using Pacifica.API.Services.EmployeeService;

namespace Pacifica.API.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)] // Exclude this controller from Swagger UI
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private  readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetEmployeeById(string id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<EmployeeDto>>>> GetAllEmployees()
        {
            var response = await _employeeService.GetAllEmployeesAsync();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> UpdateEmployee(string id, UpdateEmployeeDto updateDto)
        {
            var response = await _employeeService.UpdateEmployeeAsync(id, updateDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> CreateEmployee(RegisterDto registerDto)
        {
            var response = await _employeeService.CreateEmployeeAsync(registerDto);
            return Ok(response);
        }

        // GET: api/Employee
        [HttpGet("page")]
        public async Task<ActionResult<ApiResponse<IEnumerable<GetFilter_Employee>>>> GetEmployeesByPageAsync(
            [FromQuery] int? page = 1,
            [FromQuery] int? pageSize = 5,
            [FromQuery] string sortField = "department",  // Default sort field
            [FromQuery] int sortOrder = 1  // Default sort order (1 = ascending, -1 = descending)
        )
        {
            // Check if page or pageSize are not provided
            if (!page.HasValue || !pageSize.HasValue)
            {
                return BadRequest(new ApiResponse<IEnumerable<GetFilter_Employee>>
                {
                    Success = false,
                    Message = "Page and pageSize parameters are required."
                });
            }

            // Ensure page and pageSize are valid
            if (page < 1) return BadRequest(new ApiResponse<IEnumerable<GetFilter_Employee>>
            {
                Success = false,
                Message = "Page must be greater than or equal to 1."
            });

            if (pageSize < 1) return BadRequest(new ApiResponse<IEnumerable<GetFilter_Employee>>
            {
                Success = false,
                Message = "PageSize must be greater than or equal to 1."
            });

            // List of valid sort fields for employees
            var validSortFields = new List<string> { "employeeid", "firstname", "lastname", "email","department", "position" }; // Add more fields as needed

         
                if (!validSortFields.Contains(sortField))
            {
                return BadRequest(new ApiResponse<IEnumerable<GetFilter_Employee>>
                {
                    Success = false,
                    Message = "Invalid sort field.",
                    Data = null,
                    TotalCount = 0
                });
            }

            // Call service method to get employees by page, passing sortField and sortOrder
            var response = await _employeeService.GetEmployeesByPageAsync(page.Value, pageSize.Value, sortField, sortOrder);

            // Map the data to EmployeeDto
            var employeeDtos = _mapper.Map<IEnumerable<GetFilter_Employee>>(response.Data);

            return Ok(new ApiResponse<IEnumerable<GetFilter_Employee>>
            {
                Success = response.Success,
                Message = response.Message,
                Data = employeeDtos,
                TotalCount = response.TotalCount
            });
        }


    }

}