using asp_full_stack_api.Data;
using asp_full_stack_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_full_stack_api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _DbContext; 

        public EmployeesController(FullStackDbContext DbContext)
        {
            this._DbContext = DbContext;
        }

        [HttpGet]
        public async Task<IActionResult> getAllEmployees()
        {
          var employees = await  _DbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> addNewEployee([FromBody] Employee  employee)
        {
            employee.Id = Guid.NewGuid();
            await _DbContext.Employees.AddAsync(employee);
            await _DbContext.SaveChangesAsync();
            return Ok(employee)   ;

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getEmployee([FromRoute]Guid id)
        {
          var employee  = await  _DbContext.Employees.FirstOrDefaultAsync(c => c.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateEmployee([FromRoute] Guid id,Employee updatedEmployee)
        {
            var employee = await _DbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.Phone = updatedEmployee.Phone;
            employee.Salary = updatedEmployee.Salary;
            employee.Department = updatedEmployee.Department;
            await _DbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteEmployee([FromRoute] Guid id)
        {
            var employee = await _DbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _DbContext.Employees.Remove(employee); 
            await _DbContext.SaveChangesAsync();
            return Ok(employee);
        }




    }
}
