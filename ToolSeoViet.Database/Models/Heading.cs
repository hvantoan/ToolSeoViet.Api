using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Database.Models {
    public partial class Heading {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Title> Titles { get; set; }
    }


    public partial 
}
