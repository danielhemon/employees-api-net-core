using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Employee;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public EmployeeController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var _tempEmployees = await _context.Empleados.ToListAsync();
            var _employees = _tempEmployees.Select(s => s.ToEmployeeDto());
            return Ok(_employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var _employee = await _context.Empleados.FindAsync(id);

            if (_employee == null)
            {
                return NotFound();
            }
            return Ok(_employee.ToEmployeeDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequestDto employeeDto)
        {
            var employeeModel = employeeDto.ToEmployeeFromCreateDto();
            await _context.Empleados.AddAsync(employeeModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = employeeModel.Id }, employeeModel.ToEmployeeDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEmployeeRequestDto updateDto)
        {
            var _employee = await _context.Empleados.FirstOrDefaultAsync(X => X.Id == id);

            if (_employee == null)
            {
                return NotFound();
            }

            _employee.Name = updateDto.Name;
            _employee.Position = updateDto.Position;
            _employee.Description = updateDto.Description;
            _employee.Active = updateDto.Active;
            await _context.SaveChangesAsync();
            return Ok(_employee.ToEmployeeDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var _employee = await _context.Empleados.FirstOrDefaultAsync(X => X.Id == id);

            if (_employee == null)
            {
                return NotFound();
            }

            _context.Empleados.Remove(_employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}