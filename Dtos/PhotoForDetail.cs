﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApplication.Dtos
{
    public class PhotoForDetail
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsMain { get; set; }
    }
}
