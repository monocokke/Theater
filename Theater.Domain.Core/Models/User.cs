﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Theater.Domain.Core.Models
{
    public class User : IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }

        public Actor Actor { get; set; }
    }
}
 