using System.Collections.Generic;
using ToolSeoViet.Database.Models;

namespace ToolSeoViet.Service.Interfaces {
    public interface IViDictionaryService {
        List<ViDictionary> All();
        void InsertKeyWord(ViDictionary viDictionary);
    }
}
