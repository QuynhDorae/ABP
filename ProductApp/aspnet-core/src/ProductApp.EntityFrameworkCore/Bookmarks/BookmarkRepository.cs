﻿using Microsoft.EntityFrameworkCore;
using ProductApp.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProductApp.Bookmarks
{
    public class BookmarkRepository : EfCoreRepository<ProductAppDbContext, Bookmark, int>, IBookmarkRepository
    {
        public BookmarkRepository(IDbContextProvider<ProductAppDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Bookmark> Create(int documentId, int pageId)
        {
            var dbSet = await GetDbSetAsync();
            var bookmark = new Bookmark
            {
                DocumentId = documentId,
                PageId = pageId
            };
            await InsertAsync(bookmark, autoSave: true);
            return bookmark;
        }

        public async Task<List<Bookmark>> GetAllBookmark()
        {
            var dbSet = await GetDbSetAsync();
            var a = await dbSet.ToListAsync();
            return a;
        }
        public async Task<Bookmark> FindByDocumentIdAndPageId(int documentId, int pageId)
        {
            var dbSet = await GetDbSetAsync();
            var a = await dbSet.FirstOrDefaultAsync(x => x.DocumentId == documentId && x.PageId == pageId);
            return a;
        }
        public async Task DeleteBookmark(int id)
        {
            await DeleteAsync(id, autoSave: true);
        }
        public async Task<List<Bookmark>> GetByDocumentId(int documentId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(d => d.DocumentId == documentId)
                .ToListAsync();
        }
    }

}
