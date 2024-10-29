using AutoMapper;
using DogReviewApp.Dto;
using DogReviewApp.Model;

namespace DogReviewApp.Helper
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Dog, Dogdto>().ReverseMap();
			CreateMap<Dog, Categorydto>().ReverseMap();
			CreateMap<Categorydto, Category>().ReverseMap();
			CreateMap<Country, CountryDto>().ReverseMap();
			CreateMap<OwnerDto, Owner>().ReverseMap();
			CreateMap<Review, ReviewDto>().ReverseMap();
			CreateMap<Reviewer, ReviewerDto>().ReverseMap();
		}
	}
}
