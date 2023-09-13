namespace MDDReservationAPI.Profile
{
    public class SchoolClassProfile : AutoMapper.Profile
    {
        public SchoolClassProfile()
        {
            CreateMap<DTO.SchoolClassCreationDTO, Models.SchoolClass>();
        }
    }
}
