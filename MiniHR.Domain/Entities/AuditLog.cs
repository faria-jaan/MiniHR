using System;
using System.Collections.Generic;
using System.Text;

namespace MiniHR.Domain.Entities
{
    class AuditLog
    {
        public int LogID { get; set; }
        public string ActionType { get; set; }
        public string EntityName { get; set; }
        public int EntityID { get; set; }
        public DateTime Timestamp { get; set; }
        public string PerformedBy { get; set; }
    }
}
