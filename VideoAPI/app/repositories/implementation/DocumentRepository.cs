using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideoAPI.app.models;
using VideoAPI.Infrastructure;
using VideoAPI.Infrastructure.security.repository;

namespace VideoAPI.app.repositories.implementation
{
    public class DocumentRepository : AbstractRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(DataContext _context) : base(_context)
        {
        }

        public async Task<List<Document>> FindAllAsyncWithInclude()
        {
            return await entity.Include(d => d.StreamingUrls).ThenInclude(s => s.StreamingUrl).ToListAsync();
        }
        public override async Task<Document> FindByIdAsync(long id)
        {
            return await entity.Include(d => d.StreamingUrls).ThenInclude(s => s.StreamingUrl).SingleOrDefaultAsync(d => d.Id == id);
        }
    }
}