using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Interfaces;

namespace ToolSeoViet.Service.Utils {
    public class CacheManager {
        private readonly IMemoryCache cache;
        private readonly IViDictionaryService viDictionaryService;
        public Dictionary<string, ViDictionary> GetViDictionaries{
            get {
                Dictionary<string, ViDictionary> dictionaries = new();
                //if (false && cache.TryGetValue("ObjDicLib", out dictionaries)) {
                //    return (Dictionary<string, ViDictionary>)cache.Get("ObjDicLib");
                //} else {
                //    dictionaries = viDictionaryService.All().ToDictionary(s => s.Word, s => s);
                //    cache.Set("ObjDicLib", dictionaries);
                //}
                //TODO: Remove line below
                dictionaries = viDictionaryService.All().ToDictionary(s => s.Word, s => s);
                return dictionaries;
            }
        }
        public CacheManager(IMemoryCache memoryCache, IViDictionaryService viDictionaryService) {
            this.cache = memoryCache;
            this.viDictionaryService = viDictionaryService;
        }
    }
}
