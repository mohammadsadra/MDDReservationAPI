namespace MDDReservationAPI.Profile
{
    public class ManagerProfile : AutoMapper.Profile
    {
        public ManagerProfile()
        {
            CreateMap<DTO.ManagerCreationDTO, Models.Manager>();
        }
    }
}
