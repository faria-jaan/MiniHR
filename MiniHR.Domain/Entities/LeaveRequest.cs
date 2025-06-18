using System;
using System.Collections.Generic;
using System.Text;

namespace MiniHR.Domain.Entities
{
    class LeaveRequest
    {
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
