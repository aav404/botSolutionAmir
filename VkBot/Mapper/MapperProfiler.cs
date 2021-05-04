using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Dtos;
using VkNet.Model;

namespace VkBot.Mapper
{
    public class MapperProfiler : Profile
    {
        public MapperProfiler()
        {
            CreateMap<MessageDto, Message>()
           .ForMember(d => d.UserId, s => s.MapFrom(m => m.UserId))
           .ForAllOtherMembers(f => f.Ignore());
        }
    }
}
