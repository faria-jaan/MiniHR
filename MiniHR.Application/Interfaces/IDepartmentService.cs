using MiniHR.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniHR.Application.Interfaces
{
   public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto> GetByIdAsync(int id);
        Task CreateAsync(DepartmentDto dto, string performedBy);
        Task UpdateAsync(DepartmentDto dto, string performedBy);
        Task DeleteAsync(int id, string performedBy);
    }
}
