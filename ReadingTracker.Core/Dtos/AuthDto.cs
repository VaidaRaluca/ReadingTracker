﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Dtos
{
    public class AuthDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
