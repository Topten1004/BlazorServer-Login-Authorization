using AutoMapper;
using BlazorBaseApp.Data;
using BlazorBaseApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBaseApp.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PersonModel, Person>().ReverseMap();
        }
    }
}
