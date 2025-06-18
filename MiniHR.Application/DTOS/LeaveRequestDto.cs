using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MiniHR.Application.DTOs
{
   public class LeaveRequestDto
    {
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Leave type is required.")]
        public string LeaveType { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string HeadOfDepartment { get; set; }
    }
}
