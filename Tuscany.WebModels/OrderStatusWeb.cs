﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuscany.WebModels
{
    public class OrderStatusWeb
    {
        public int Id { get; set; }

        public string Status { get; set; } = null!;
    }
}