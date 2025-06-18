using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MiniHR.Application.DTOs
{
    public class DepartmentDto
    {
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(100, ErrorMessage = "Department Name cannot exceed 100 characters")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Head of Department is required")]
        [StringLength(100, ErrorMessage = "Head of Department cannot exceed 100 characters")]
        public string HeadOfDepartment { get; set; }
    }
}
