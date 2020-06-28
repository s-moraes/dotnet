using System;
using System.Collections.Generic;

namespace DbFirst.Models
{
    public partial class TblUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Password { get; set; }
    }
}
