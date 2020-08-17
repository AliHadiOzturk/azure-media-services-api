using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoAPI.app.models;
using VideoAPI.app.services.models;

namespace VideoAPI.Infrastructure
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // CreateMap<Document, GetDocumentResponse>().ForMember(d => d.StreamingUrls, opt => opt.MapFrom(src => src.StreamingUrls.Select(s => s.StreamingUrl).ToList()));
            CreateMap<Document, GetDocumentResponse>().ForMember(src => src.StreamingUrls, opt => opt.MapFrom(src => src.StreamingUrls.Select(s => s.StreamingUrl).ToList()));
            CreateMap<Document, DocumentListItem>();
            CreateMap<StreamingUrl, StreamUrlListItem>();
        }
    }
}
