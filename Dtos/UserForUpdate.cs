﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApplication.Dtos
{
    public class UserForUpdate
    {
        public string Introduction { get; set; }
        public string LookingFor  { get; set; }
        public string Interest { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
