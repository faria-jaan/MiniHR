using System;
using System.Collections.Generic;
using System.Text;

namespace MiniHR.Domain.Entities
{
    class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int DepartmentID { get; set; }
        public bool IsActive { get; set; }
    }
}
