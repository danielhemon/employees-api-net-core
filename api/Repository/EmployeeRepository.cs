using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Employee;
using api.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDBContext _context;

        public EmployeeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Empleados.ToListAsync();
        }

        public async Task<Employee> CreateAsync(Employee employeeModel)
        {
            await _context.Empleados.AddAsync(employeeModel);
            await _context.SaveChangesAsync();

            return employeeModel;
        }

        public async Task<Employee?> DeleteAsync(int id)
        {
            var employeeModel = await _context.Empleados.FirstOrDefaultAsync(x => x.Id == id);

            if (employeeModel == null)
            {
                return null;
            }

            _context.Empleados.Remove(employeeModel);
            await _context.SaveChangesAsync();
            return employeeModel;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Empleados.FindAsync(id);
        }

        public async Task<Employee?> UpdateAsync(int id, UpdateEmployeeRequestDto employeeDto)
        {
            var _employee = await _context.Empleados.FirstOrDefaultAsync(x => x.Id == id);

            if (_employee == null)
            {
                return null;
            }

            _employee.Name = employeeDto.Name;
            _employee.Position = employeeDto.Position;
            _employee.Description = employeeDto.Description;
            _employee.Active = employeeDto.Active;
            await _context.SaveChangesAsync();
            return _employee;
        }
    }
}