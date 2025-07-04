﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core.Dtos
{
    public class ReaderDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
