using AutoMapper;
using MVCDemo.Dtos;
using MVCDemo.Models;

namespace MVCDemo
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Domaim Model to Dto
            CreateMap<Customer, CustomerDto>();
               // .ForMember(o => o.MembershipTypeId, m => m.MapFrom(x => x.MembershipTypeId)); ;
            CreateMap<MemberShipType, MemberShipTypeDto>();
            CreateMap<Movie, MovieDto>();

            //Dto to Domain Model
            CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<MovieDto, Movie>()
                .ForMember(m => m.Id, opt => opt.Ignore());
            
            //Mapper.CreateMap<Customer, CustomerDto>();
            //Mapper.CreateMap<CustomerDto, Customer>();
        }
    }
}
