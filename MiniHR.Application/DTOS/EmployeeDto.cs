using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniHR.Application.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid Phone number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Date of Joining is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a department.")]
        public int DepartmentID { get; set; }

        public bool IsActive { get; set; }

        // This is used to populate dropdowns in the view; no validation needed.
        public List<DepartmentDto> Departments { get; set; }
    }
}
