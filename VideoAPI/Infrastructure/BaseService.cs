// using AutoMapper;

using AutoMapper;

namespace VideoAPI.Infrastructure
{
    public class BaseService
    {
        protected readonly IMapper mapper;

        public BaseService(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}