﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Service.Models.Auth
{
    public class SignUpRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }



}