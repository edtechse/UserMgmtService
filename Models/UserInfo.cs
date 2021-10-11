using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement_Service.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserEmailAddress { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string BadgeIds { get; set; }
        public string Genres { get; set; }
        public bool InUse { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public string CreateId { get; set; }
        public DateTime UpdateTimestamp { get; set; }
        public string UpdateId { get; set; }

    }
}
