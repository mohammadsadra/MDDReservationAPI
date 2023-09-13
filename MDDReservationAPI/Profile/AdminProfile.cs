namespace MDDReservationAPI.Profile
{
    public class AdminProfile : AutoMapper.Profile
    {
        public AdminProfile()
        {
            CreateMap<DTO.AdminCreationDTO, Models.Admin>();
        }
    }
}
