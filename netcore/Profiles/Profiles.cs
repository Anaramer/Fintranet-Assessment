using AutoMapper;

namespace congestion.calculator.Profiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Entities.CityEntity, DTOs.CityDto>().ReverseMap();

            CreateMap<Entities.TollFeeEntity, DTOs.TollFeeDto>().ReverseMap();

            CreateMap<Entities.TollFreeDateEntity, DTOs.TollFreeDateDto>().ReverseMap();
            CreateMap<Entities.TollFreeDayOfWeekEntity, DTOs.TollFreeDayOfWeekDto>().ReverseMap();
            CreateMap<Entities.TollFreeMonthEntity, DTOs.TollFreeMonthDto>().ReverseMap();
            CreateMap<Entities.TollFreeVehicleEntity, DTOs.TollFreeVehicleDto>().ReverseMap();
        }
    }
}
