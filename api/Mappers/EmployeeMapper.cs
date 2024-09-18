using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Employee;
using api.Models;

namespace api.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto ToEmployeeDto(this Employee employeeModel)
        {
            return new EmployeeDto
            {
                Id = employeeModel.Id,
                Name = employeeModel.Name,
                Position = employeeModel.Position,
                Description = employeeModel.Description,
                Active = employeeModel.Active
            };
        }

        public static Employee ToEmployeeFromCreateDto(this CreateEmployeeRequestDto createEmployeeDto)
        {
            return new Employee
            {
                Name = createEmployeeDto.Name,
                Position = createEmployeeDto.Position,
                Description = createEmployeeDto.Description,
                Active = createEmployeeDto.Active
            };
        }
    }
}