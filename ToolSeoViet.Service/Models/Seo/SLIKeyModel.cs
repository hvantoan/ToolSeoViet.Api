using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSeoViet.Service.Models.Seo {
    public class SLIKeyModel {
        public int Count { get; set; }
        public string KeyWord { get; set; }
        public string KeyAndCount {
            get {
                string x = KeyWord + "(" + Count + ")";
                return x;
            }
        }
    }
}
