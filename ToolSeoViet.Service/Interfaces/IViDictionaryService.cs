using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Database.Models;

namespace ToolSeoViet.Service.Interfaces {
    public interface IViDictionaryService {
        List<ViDictionary> All();
        void InsertKeyWord(ViDictionary viDictionary);
    }
}
