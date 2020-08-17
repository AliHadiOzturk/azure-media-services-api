using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using VideoAPI.app.models;
using VideoAPI.app.repositories;
using VideoAPI.app.services.models;
using VideoAPI.Infrastructure;

namespace VideoAPI.app.services
{
    public class ContentService : BaseService
    {
        private readonly IConfiguration configuration;
        private readonly IDocumentRepository documentRepository;
        private readonly ContentDomainService contentDomainService;

        public ContentService(IMapper mapper, IConfiguration configuration,
                              IDocumentRepository documentRepository,
                              ContentDomainService contentDomainService) : base(mapper)
        {
            this.contentDomainService = contentDomainService;
            this.configuration = configuration;
            this.documentRepository = documentRepository;
        }


        
        public async Task CreateContent(IFormFile file)
        {
            string uploadFolder = configuration.GetValue<string>("uploadFolder");
            int value = new Random().Next(900) + 10;
            var filePath = uploadFolder + "/files/" + value + "_" + file.FileName;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            Document document = new Document()
            {
                FileName = Path.GetFileNameWithoutExtension(filePath),
                Path = filePath,
            };

            document = await Task.Run(() => documentRepository.Save(document));

            await contentDomainService.UploadDocument(document);
        }
        public async Task publishDocument(long documentId)
        {
            Document document = await documentRepository.FindByIdAsync(documentId);
            await contentDomainService.PublishDocument(document);
        }
    }
}