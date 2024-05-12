using AutoMapper;

namespace API.BusinessLogic
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Models.RealEstate, DbAccess.Models.RealEstateModel>()
            .ForMember(rem => rem.OwnerId, re => re.MapFrom(re => re.Owner.Id))
            .ForMember(rem => rem.RealEstateTypeId, re => re.MapFrom(re => re.Type.Id))
            .ForMember(rem => rem.OccupiedById, re =>
            {
                re.PreCondition(re => re.OccupiedBy != null);
                re.MapFrom(re => re.OccupiedBy!.Id);
            }).ReverseMap();

            CreateMap<Models.User, DbAccess.Models.UserModel>()
            .ForMember(um => um.OwnerTypeId, u => u.MapFrom(u => u.OwnerType.Id)).ReverseMap();

            CreateMap<Models.UserViewModel, Models.User>().ReverseMap();

            CreateMap<DbAccess.Models.UserModel, Models.UserViewModel>();

            CreateMap<Models.RealEstateType, DbAccess.Models.RealEstateTypeModel>().ReverseMap();

            CreateMap<Models.OwnerType, DbAccess.Models.OwnerTypeModel>().ReverseMap();
        }
    }
}