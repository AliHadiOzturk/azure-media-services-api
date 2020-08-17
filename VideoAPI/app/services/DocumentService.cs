using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using VideoAPI.app.models;
using VideoAPI.app.repositories;
using VideoAPI.app.services.models;
using VideoAPI.Infrastructure;

namespace VideoAPI.app.services
{
    public class DocumentService : BaseService
    {
        private readonly IDocumentRepository documentRepository;

        public DocumentService(IMapper mapper,
                               IDocumentRepository documentRepository) : base(mapper)
        {
            this.documentRepository = documentRepository;
        }

        public async Task<List<DocumentListItem>> GetDocumentListItemsAsync()
        {
            List<Document> documents = await documentRepository.FindAllAsyncWithInclude();
            List<DocumentListItem> documentListItems = mapper.Map<List<DocumentListItem>>(documents);

            return documentListItems;
        }

        public async Task<GetDocumentResponse> GetDocumentResponseAsync(long documentId)
        {
            Document doc = await documentRepository.FindByIdAsync(documentId);

            GetDocumentResponse response = mapper.Map<GetDocumentResponse>(doc);

            return response;

        }
    }
}