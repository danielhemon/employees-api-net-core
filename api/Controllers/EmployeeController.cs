using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Employee;
using api.interfaces;
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
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(ApplicationDBContext context, IEmployeeRepository employeeRepo)
        {
            _context = context;
            _employeeRepo = employeeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _tempEmployees = await _employeeRepo.GetAllAsync();
            var _employees = _tempEmployees.Select(s => s.ToEmployeeDto());
            return Ok(_employees);
        }

        [HttpGet("{id: int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _employee = await _employeeRepo.GetByIdAsync(id);

            if (_employee == null)
            {
                return NotFound();
            }
            return Ok(_employee.ToEmployeeDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequestDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employeeModel = employeeDto.ToEmployeeFromCreateDto();
            await _employeeRepo.CreateAsync(employeeModel);
            return CreatedAtAction(nameof(GetById), new { id = employeeModel.Id }, employeeModel.ToEmployeeDto());
        }


        [HttpPut]
        [Route("{id: int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEmployeeRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _employee = await _employeeRepo.UpdateAsync(id, updateDto);

            if (_employee == null)
            {
                return NotFound();
            }

            return Ok(_employee.ToEmployeeDto());
        }

        [HttpDelete]
        [Route("{id: int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _employee = await _employeeRepo.DeleteAsync(id);

            if (_employee == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}