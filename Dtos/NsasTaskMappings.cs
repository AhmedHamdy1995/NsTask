using AutoMapper;
using NsTask.Api.Dtos.NsasTask;

namespace NsTask.Api.Dtos
{
    public class NsasTaskMappings : Profile
    {
        public NsasTaskMappings()
        {
            CreateMap<Domain.Enteties.NsasTask, NsasTaskDto>().ReverseMap();
        }
    }
}
