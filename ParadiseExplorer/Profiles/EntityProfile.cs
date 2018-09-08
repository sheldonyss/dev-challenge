using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParadiseExplorer.Domains;
//using ParadiseExplorer.Domains;

namespace ParadiseExplorer.Models
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Entity, EntityDto>();
        }
    }
}
