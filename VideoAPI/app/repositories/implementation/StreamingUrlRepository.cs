using System.Collections.Generic;
using System.Threading.Tasks;
using VideoAPI.app.models;
using VideoAPI.Infrastructure;
using VideoAPI.Infrastructure.security.repository;

namespace VideoAPI.app.repositories.implementation
{
    public class StreamingUrlRepository : AbstractRepository<StreamingUrl>, IStreamingUrlRepository
    {
        public StreamingUrlRepository(DataContext _context) : base(_context)
        {
        }

        public Task<List<StreamingUrl>> FindAllAsyncWithInclude()
        {
            throw new System.NotImplementedException();
        }
    }
}