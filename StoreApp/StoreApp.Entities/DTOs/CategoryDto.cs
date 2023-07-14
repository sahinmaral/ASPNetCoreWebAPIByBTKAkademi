﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Entities.DTOs
{
    public record CategoryDto
    {
        public int Id { get; init; }
        public string? Name { get; init; }
    }
}
