using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Service.Models.Seo {
    public partial class SearchPosition {
        public string Key { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string Href { get; set; }
    }
}
