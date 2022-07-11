using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Interfaces;

namespace ToolSeoViet.Service.Utils {
    public class CacheManager {
        private IMemoryCache cache;
        private IViDictionaryService viDictionaryService;
        public Dictionary<string, ViDictionary> GetViDictionaries{
            get {
                Dictionary<string, ViDictionary> dictionaries;
                if (cache.TryGetValue("dictionaries", out dictionaries)) {
                    return (Dictionary<string, ViDictionary>)cache.Get("dictionaries");
                } else {
                    dictionaries = viDictionaryService.All().ToDictionary(s => s.Word, s => s);
                    cache.Set("dictionaries", dictionaries);
                }
                return dictionaries;
            }
        }
        public CacheManager(IMemoryCache memoryCache, IViDictionaryService viDictionaryService) {
            this.cache = memoryCache;
            this.viDictionaryService = viDictionaryService;
        }
    }
}
