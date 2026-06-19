using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        // CREATE
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Email))
                throw new Exception("Email is required");
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        // GET ALL
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees
                .AsNoTracking()
                .ToListAsync();
        }

        // GET BY ID
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        // UPDATE
        public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
        {
            var existing = await _context.Employees.FindAsync(id);

            if (existing == null)
                return false;

            existing.Name = employee.Name;
            existing.Email = employee.Email;
            existing.Department = employee.Department;
            existing.Salary = employee.Salary;

            await _context.SaveChangesAsync();
            return true;
        }
        // DELETE
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return false;
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
