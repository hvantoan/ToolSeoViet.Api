using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void InsertKeyWord(ViDictionary dictionary) {
            db.ViDictionaries.Add(new ViDictionary() {
                Id = Guid.NewGuid().ToStringN(),
                Description = dictionary.Description,
                Word = dictionary.Word,
                IsMeaning = dictionary.IsMeaning,
            });
            db.SaveChanges();
        }
    }
}
