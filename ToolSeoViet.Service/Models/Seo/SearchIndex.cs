﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ToolSeoViet.Service.Enums;

namespace ToolSeoViet.Service.Models.Seo {
    public partial class SearchIndex {
        public string Href { get; set; }
        public ECheckIndex Status { get; set; }
    }
}