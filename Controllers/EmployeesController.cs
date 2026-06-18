using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementAPI.Data;
namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return _context.Employees.ToList();
        }
        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();

            return Ok(emp);
        }
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee updatedEmp)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
                return NotFound();

            employee.Name = updatedEmp.Name;
            employee.Department = updatedEmp.Department;
            employee.Salary = updatedEmp.Salary;

            _context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return NoContent();
        }


    }
}

