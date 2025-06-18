using System;
using System.Collections.Generic;
using System.Text;

namespace MiniHR.Domain.Entities
{
    class LeaveBalance
    {
        public int EmployeeID { get; set; }
        public int AnnualLeave { get; set; }
        public int SickLeave { get; set; }
        public int UnpaidLeave { get; set; }
    }
}
