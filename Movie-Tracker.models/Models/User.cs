using System;
using System.Collections.Generic;

namespace Movie_Tracker.models.Models
{
    public partial class User
    {
        public string UserId { get; set; } = null!;
        public string? Password { get; set; }
    }
}
