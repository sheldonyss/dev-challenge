using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParadiseExplorer.Domains;
using ParadiseExplorer.Models;

namespace ParadiseExplorer.Profiles
{
    public class OfficerProfile : Profile
    {
        public OfficerProfile()
        {
            CreateMap<Officer, OfficerDto>();
        }
    }
}
