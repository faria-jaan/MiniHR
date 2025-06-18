using System;
using System.Collections.Generic;
using System.Text;

namespace MiniHR.Application.DTOs
{
    public class LeaveBalanceDto
    {
        public int LeaveBalanceID { get; set; }
        public int EmployeeID { get; set; }
        public int AnnualLeave { get; set; }
        public int SickLeave { get; set; }
        public int UnpaidLeave { get; set; }

        // Extra fields returned from your SP
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string HeadOfDepartment { get; set; }
    }
}
