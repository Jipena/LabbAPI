using AutoMapper;
using LabbAPI.Models;
using LabbAPI.Models.DTO;

namespace LabbAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()  // constructor
        {
            // Person
            CreateMap<Person, PersonDto>().ReverseMap();   // ReverseMap gör en kopia och spegelvänder mappningen
            CreateMap<Person, PersonCreateDto>().ReverseMap();
            CreateMap<Person, PersonUpdateDto>().ReverseMap();

            // Interest
            CreateMap<Interest, InterestDto>().ReverseMap();
            CreateMap<Interest, InterestCreateDto>().ReverseMap();
            CreateMap<Interest, InterestUpdateDto>().ReverseMap();

            // PersonInterest
            CreateMap<PersonInterest, PersonInterestDto>().ReverseMap();
            //CreateMap<PersonInterest, PersonInterestCreateDto>().ReverseMap();
            //CreateMap<PersonInterest, PersonInterestUpdateDto>().ReverseMap();

            //// WebURL
            CreateMap<WebURL, WebURLDto>().ReverseMap();
            //CreateMap<WebURL, WebURLCreateDto>().ReverseMap();
            //CreateMap<WebURL, WebURLUpdateDto>().ReverseMap();
        }
    }
}
