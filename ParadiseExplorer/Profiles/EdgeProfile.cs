using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParadiseExplorer.Domains;
using ParadiseExplorer.Models;

namespace ParadiseExplorer.Profiles
{
    public class EdgeProfile : Profile
    {
        public EdgeProfile()
        {
            CreateMap<Edges, EdgeDto>();
        }
    }
}
