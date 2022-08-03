﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarEfta.BL.Helper
{
    public class ApiResponse<T>
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }

    }
}
