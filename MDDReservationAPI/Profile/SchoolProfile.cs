namespace MDDReservationAPI.Profile
{
    public class SchoolProfile: AutoMapper.Profile
    {
        public SchoolProfile()
        {
            CreateMap<DTO.SchoolCreationDTO, Models.School>();
        }
    }
}
