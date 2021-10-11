using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement_Service.Models
{
    public class Badge
    {
        public int BadgeId { get; set; }
        public string BadgeName { get; set; }
        public string BadgeDescription { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public string CreateId { get; set; }
        public DateTime UpdateTimestamp { get; set; }
        public string UpdateId { get; set; }
    }
}
