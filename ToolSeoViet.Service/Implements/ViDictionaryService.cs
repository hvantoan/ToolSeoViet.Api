using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ToolSeoViet.Database;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Interfaces;
using ToolSeoViet.Services.Common;
using TuanVu.Services.Extensions;

namespace ToolSeoViet.Service.Implements {
    public class ViDictionaryService : BaseService, IViDictionaryService {


        public ViDictionaryService(ToolSeoVietContext db, IHttpContextAccessor httpContextAccessor)
            : base(db, httpContextAccessor) {
        }
        public List<ViDictionary> All() {

            return db.ViDictionaries.AsQueryable().ToList();
        }

        public async Task InsertKeyWord(ViDictionary dictionary) {
            bool isExiting = db.ViDictionaries.Any(o => o.Word.Equals(dictionary.Word));
            if (isExiting) return;
            try {
                db.ViDictionaries.Add(new ViDictionary() {
                    Id = Guid.NewGuid().ToStringN(),
                    Description = dictionary.Description,
                    Word = dictionary.Word,
                    IsMeaning = dictionary.IsMeaning,
                });
                await db.SaveChangesAsync();
            } catch (Exception ex) { }

        }
    }
}
